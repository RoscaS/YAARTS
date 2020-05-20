using Components.State;
using Game.Enums;

public class EngagingState : IState
{
    private readonly Entity entity;
    private bool targetIsCharacter;

    public EngagingState(Entity entity) {
        this.entity = entity;
    }

    public void Update() {

        if (entity.Interaction.TargetFinished() || entity.Interaction.TargetInRange()) {
            entity.ClearDestination();
        }
        else {
            var destination = targetIsCharacter
                    ? entity.Interaction.Target.Position
                    : entity.Interaction.closestPoint;
            entity.SetDestination(destination);
        }
    }

    public void OnEntry() {
        entity.Animation.SetMoveAnimation();
        targetIsCharacter = entity.Interaction.Target.Meta.Type == EntityType.Character;

        if (!targetIsCharacter) {
            entity.Interaction.UpdateClosestPoint();
        }
    }

    public void OnExit() { }

    public override string ToString() => "Engaging";
}
