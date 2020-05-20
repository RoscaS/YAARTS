using System;
using System.Collections.Generic;
using UnityEngine;


namespace Components.State
{
    public class CharacterStateComponent : BaseStateComponent
    {

        protected override void InitStates() {
            states = new Dictionary<Type, IState> {
                    { typeof(IdleState), new IdleState(entity) },
                    { typeof(MovingState), new MovingState(entity) },
                    { typeof(EngagingState), new EngagingState(entity) },
                    { typeof(EngagedState), new EngagedState(entity) },
            };
        }

        public override void Update() {
            entity.Animation.UpdateMoveAnimationSpeedValue(entity.Speed);
            CheckConditions();
            base.Update();
        }

        /*------------------------------------------------------------------*\
        |*							CONDITIONS
        \*------------------------------------------------------------------*/

        private void CheckConditions() {
            var haveTarget = entity.Interaction.Target != null;
            var haveDestination = entity.Movable.Destination != null;

            if (Current == null) {
                SetState(states[typeof(IdleState)]);
            }

            else if (!haveTarget && !haveDestination) {
                if (Current.GetType() != typeof(IdleState)) {
                    SetState(states[typeof(IdleState)]);
                }
            }

            else if (!haveTarget) {
                if (Current.GetType() != typeof(MovingState)) {
                    SetState(states[typeof(MovingState)]);
                }
            }

            else if (haveDestination) {
                if (Current.GetType() != typeof(EngagingState)) {
                    SetState(states[typeof(EngagingState)]);
                }
            }

            else {
                if (Current.GetType() != typeof(EngagedState)) {
                    SetState(states[typeof(EngagedState)]);
                }
            }
        }
    }
}
