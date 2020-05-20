using System;
using Game.Entities;
using Game.Enums;
using UnityEngine;
using Utils;

namespace Components.Interaction
{
    public abstract class BaseInteractionComponent : MonoBehaviour
    {
        protected Entity entity;

        public Entity Target;
        [HideInInspector] public float DPS;
        [HideInInspector] public bool IsInteracting;
        [HideInInspector] public float baseDamages;

        public float Range;
        public float Damages;
        public float CoolDown;


        // Used for big (as in big size) targets
        [HideInInspector] public Vector3 closestPoint;

        /*------------------------------------------------------------------*\
        |*							PROPS
        \*------------------------------------------------------------------*/

        public bool TargetFinished() => !Target || Target.disabled;

        public bool TargetInRange() {
            var destination = Target.Meta.Type == EntityType.Character
                    ? Target.Position
                    : closestPoint;

            return Vector3.Distance(entity.Position, destination) <= Range;
        }

        /*------------------------------------------------------------------*\
        |*							CONSTRUCTOR
        \*------------------------------------------------------------------*/

        protected virtual void Start() {
            entity = GetComponent<Entity>();
            DPS = (float) Math.Round(Damages / CoolDown, 1);
            baseDamages = Damages;
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public void UpdateClosestPoint() {
            if (Target.Collider) {
                closestPoint = Target.Collider.ClosestPoint(entity.Position);
            }
        }

        public virtual void SetTarget(Entity target) {
            if (target != entity) {
                Target = target;
            }
        }

        public void ClearTarget() {
            Target = null;
        }

        public virtual void Interact() {
            if (IsInteracting || !Target || Target.disabled) return;
            IsInteracting = true;
            entity.Animation.SetAttackAnimation();
            entity.Movable.Stop();
            entity.HardLookAtTarget();
            FunctionDelay.Create(() => IsInteracting = false, CoolDown);
        }

        public void DrawTarget() {
            if (Target && Selection.Instance.TargetOverlay &&
                entity.Selectable.IsSelected) {
                EntityDebug.DrawTarget(entity);
            }
        }

        public override string ToString() {
            return $"dmg: {Damages} spd: {CoolDown} dps: {DPS}";
        }

        public abstract void InteractionCallback();
    }
}
