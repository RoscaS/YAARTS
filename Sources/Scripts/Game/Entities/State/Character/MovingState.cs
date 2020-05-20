public class MovingState : IState
{
    private readonly Entity Entity;
    private bool flag;

    public MovingState(Entity entity) {
        Entity = entity;
    }

    public void Update() {
        if (!Entity.Agent.pathPending) {
            if (Entity.Movable.ReachedDestination) {
                Entity.ClearDestination();
                // Entity.Agent.radius = .3f;
            }

            if (Entity.Speed <= .5f && Entity.Speed > .1f) {
                if (Entity.Movable.RemainingDistance <= 1f) {
                    Entity.ClearDestination();
                    // Entity.Agent.radius = .3f;
                }
            }
        }
    }

    public void OnEntry() {
        Entity.Animation.SetMoveAnimation();
    }

    public void OnExit() {
        Entity.Movable.ResetStoppingDistance();
    }

    public override string ToString() => "Moving";
}
