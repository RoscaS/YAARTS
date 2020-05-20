namespace DefaultNamespace
{
    public class DestroyedState : IState
    {
        private readonly Entity Entity;

        public DestroyedState(Entity entity) {
            Entity = entity;
        }

        /*------------------------------------------------------------------*\
        |*							IState IMPLEMENTATION
        \*------------------------------------------------------------------*/

        public void Update() { }
        public void OnEntry() { }
        public void OnExit() { }
    }
}
