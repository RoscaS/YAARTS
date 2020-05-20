using UnityEngine;

public class AnimatorEventsListener : MonoBehaviour
{
    private Entity entity;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    void Start() {
        entity = GetComponent<Entity>();
    }

    /*------------------------------------------------------------------*\
    |*							ANIMATOR EVENTS
    \*------------------------------------------------------------------*/

    public void MeleeAttackCallback() {
        entity.Interaction.InteractionCallback();
    }

    public void TwoHandedAttackCallback() {
        entity.Interaction.InteractionCallback();
    }

    public void PoleAttackCallback() {
        entity.Interaction.InteractionCallback();
    }

    public void ArcherAttackCallback() {
        entity.Interaction.InteractionCallback();
    }

    public void StaffAttackCallback() {
        entity.Interaction.InteractionCallback();
    }
}
