
public class IdleState : IState
{
    private readonly Entity entity;

    public IdleState(Entity entity) {
        this.entity = entity;
    }

    public void Update() {
        entity.Awareness.Scan();
    }

    public void OnEntry() {
        entity.Animation.SetIdleAnimation();
    }

    public void OnExit() {
    }

    public override string ToString() => "Idle";
}
