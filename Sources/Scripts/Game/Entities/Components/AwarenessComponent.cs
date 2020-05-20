using System;
using System.Collections.Generic;
using System.Linq;
using Game.Utils;
using UnityEngine;

namespace Components
{
    public class AwarenessComponent : MonoBehaviour
    {
        private Entity entity;
        public float ligneOfSight;


        [HideInInspector] public List<Collider> aggro;
        [HideInInspector] public Entity closest;


        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            entity = GetComponent<Entity>();
            CreateLigneOfSightMesh();
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public void Scan() {
            // if (entity.Meta.IsWorker) return;
            aggro = Scanning();

            if (aggro.Any()) {

                PrioriseTargets();

            }
        }

        /// <summary>
        /// Make sure that a fighter will priorise moving entities over structures.
        /// </summary>
        private void PrioriseTargets() {
            var mobiles = aggro.Where(i => !i.CompareTag("Structure")).ToList();

            if (mobiles.Any()) {
                aggro = mobiles;
                closest = Getclosest(aggro).GetComponent<Entity>();
            }
            else {
                closest = Getclosest(aggro).transform.parent.GetComponent<Entity>();
            }
            entity.SetTarget(closest);
        }

        public List<Collider> Scanning() {
            return Physics.OverlapSphere(
                entity.Position,
                ligneOfSight,
                LayerMask.GetMask(entity.Owner.targetLayer)
            ).ToList();
        }

        public Collider Getclosest(List<Collider> colliders) {
            // Optimisation using sqrtMagnitude instead of sqrt.
            return colliders.OrderBy(c => (c.transform.position - entity.Position).sqrMagnitude)
                            .FirstOrDefault();
        }

        public void CreateLigneOfSightMesh() {
            if (entity.Owner.IsCPU()) return;
            var LOS = Instantiate(Resources.Load("Prefabs/FX/LignOfSight") as GameObject);
            LOS.GetComponent<LOSMesh>().Entity = entity;
        }
    }
}
