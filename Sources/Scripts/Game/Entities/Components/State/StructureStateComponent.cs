using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace Components.State
{
    public class StructureStateComponent : BaseStateComponent
    {
        public int completionState = 1;
        public bool completed;

        protected override void InitStates() {
            states = new Dictionary<Type, IState> {
                    { typeof(ConstructionState), new ConstructionState(entity) },
                    { typeof(EnabledState), new EnabledState(entity) },
                    { typeof(DestroyedState), new DestroyedState(entity) },
            };

            SetState(states[typeof(EnabledState)]);
        }
    }
}
