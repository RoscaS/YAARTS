using Utils;

public class EngagedState : IState
{
    private readonly Entity entity;


    public EngagedState(Entity entity) {
        this.entity = entity;
    }

    /*------------------------------------------------------------------*\
    |*							IState IMPLEMENTATION
    \*------------------------------------------------------------------*/

    public void Update() {
        if (!entity.Interaction.TargetFinished() && entity.Interaction.TargetInRange()) {
            entity.Interaction.Interact();
        }
        else {
            FunctionDelay.Create(
                () => {
                    entity.ClearTarget();
                    entity.ClearDestination();
                },
                .4f
            );
        }
    }

    public void OnEntry() {
        entity.HardLookAtTarget();
    }

    public void OnExit() { }

    public override string ToString() => "Engaged";
}
