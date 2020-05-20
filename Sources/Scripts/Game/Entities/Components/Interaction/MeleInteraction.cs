using UnityEngine;

namespace Components.Interaction
{
    public class MeleInteraction : BaseInteractionComponent
    {

        public override void InteractionCallback() {
            if (Target) {
                var direction = Quaternion.LookRotation(-entity.HitBox.box.up);
                Target.Resource.Reduce(Damages, direction: direction, other: entity);
            }
        }
    }
}
