using Components.Resource;
using Components.State;
using UnityEngine;

namespace DefaultNamespace
{
    public class ConstructionState : IState
    {
        private readonly Entity entity;

        public ConstructionState(Entity entity) {
            this.entity = entity;
        }


        /*------------------------------------------------------------------*\
        |*							PROPS
        \*------------------------------------------------------------------*/

        private StructurePointsResource Resource => (StructurePointsResource) entity.Resource;
        private StructureStateComponent State => (StructureStateComponent) entity.State;

        /*------------------------------------------------------------------*\
        |*							IState IMPLEMENTATION
        \*------------------------------------------------------------------*/

        public void Update() {
            if (Resource.IsFull) {
                entity.State.SetState(entity.State.states[typeof(EnabledState)]);
            }
        }

        public void OnEntry() {
            State.completionState = 1;
            State.completed = false;
            Resource.Reduce(Resource.Total - 100);

            foreach (Transform model in entity.model) {
                model.gameObject.SetActive(false);
            }
            entity.model.GetChild(0).gameObject.SetActive(true);
        }


        public void OnExit() {
            State.completed = true;
            Resource.IncreaseCompletionState();
        }

        public override string ToString() => "Construction";
    }
}
