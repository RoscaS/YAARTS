using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Components
{
    public class AnimationComponent : MonoBehaviour
    {
        public static readonly int Idle = Animator.StringToHash("idle");
        public static readonly int Move = Animator.StringToHash("move");
        public static readonly int Attack = Animator.StringToHash("attack");
        public static readonly int Damages = Animator.StringToHash("damages");
        public static readonly int DeathA = Animator.StringToHash("death_a");
        public static readonly int DeathB = Animator.StringToHash("death_b");

        private Entity entity;
        private Animator Animator;


        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        public void Start() {
            entity = GetComponent<Entity>();
            Animator = entity.GetComponent<Animator>();
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public void UpdateMoveAnimationSpeedValue(float speed) {
            Animator.SetFloat(Move, speed);
        }

        public void SetIdleAnimation() {
            Animator.SetBool(Attack, false);
            Animator.SetBool(Idle, true);
        }

        public void SetMoveAnimation() {
            Animator.SetBool(Attack, false);
            Animator.SetBool(Idle, false);
        }

        public void SetAttackAnimation() {
            Animator.SetBool(Attack, true);
            FunctionDelay.Create(AttackCallback, .75f);
        }

        public void SetDamagesAnimation() {
            Animator.SetBool(Damages, true);
            FunctionDelay.Create(DamagesCallback, .1f);
        }

        public void SetDeathAnimation() {
            Animator.SetBool(Idle, false);
            Animator.SetBool(Attack, false);
            Animator.SetBool(Random.Range(0, 2) == 0 ? DeathA : DeathB, true);
        }

        /*------------------------------------------------------------------*\
        |*							CALLBACKS
        \*------------------------------------------------------------------*/

        public void AttackCallback() {
            Animator.SetBool(Attack, false);
            entity.Movable.Resume();
        }

        public void DamagesCallback() {
            Animator.SetBool(Damages, false);
            entity.Movable.Resume();
        }

    }
}
