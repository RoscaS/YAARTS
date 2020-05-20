using System;
using System.Collections;
using System.Linq;
using Game.Enums;
using UnityEngine;
using Utils;

namespace Components
{
    public class CarryComponent : MonoBehaviour
    {
        public float max = 100;
        public float current;

        private Entity entity;
        private Entity townCenter;

        [HideInInspector] public Entity collectible;
        [HideInInspector] public CollectibleType collectedType;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            entity = GetComponent<Entity>();
            FunctionDelay.Create(() => townCenter = FindTC(), .5f);
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public void Load(float quantity) {
            current += quantity;
            CreatePopup(quantity);

            if (current >= max) {
                GotoTC();
            }
        }

        public float UnLoad() {
            var temp = current;
            current = 0;
            CreatePopup(-temp);
            return temp;
        }

        public void GotoTC() {
            entity.SetTarget(townCenter);
        }

        public void GoBackToWork() {
            FunctionDelay.Create(() => entity.SetTarget(collectible), .1f);
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private Entity FindTC() {
            var mask = LayerMask.GetMask(entity.Owner.layer);

            return Physics.OverlapSphere(entity.Position, entity.Awareness.ligneOfSight, mask)
                          .Select(i => i.GetComponent<Entity>())
                          .First(i => i.Meta.Class == EntityClass.TownCenter);
        }

        private void CreatePopup(float quantity) {
            if (collectedType == CollectibleType.Structure) return;
            var sign = quantity > 0 ? "+" : "-";
            var color = quantity > 0 ? Colors.Primary : Colors.Secondary;
            var text = $"{sign} {Math.Abs(quantity)} {collectedType.ToString()}";
            var position = Helpers.SetY(entity.Position, 2f);
            DrawingTools.TextPopup(text, position, 2f, 1f, color: color);
        }
    }
}
