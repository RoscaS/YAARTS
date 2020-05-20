using DefaultNamespace;
using Game.Enums;
using UnityEngine;

namespace Components.Interaction
{
    public class WorkerInteraction : BaseInteractionComponent
    {
        public Transform pick;
        public Transform axe;
        public Transform hammer;

        public override void SetTarget(Entity target) {
            base.SetTarget(target);


            if (target.Meta.IsCollectible) {
                entity.Carry.collectible = target;
                EmptyHand();

                if (Target.Meta.Class == EntityClass.Tree) {
                    axe.gameObject.SetActive(true);
                    entity.Carry.collectedType = CollectibleType.Wood;
                }
                else if (Target.Meta.Class == EntityClass.Gold) {
                    pick.gameObject.SetActive(true);
                    entity.Carry.collectedType = CollectibleType.Gold;
                }
            }
            else if (target.Meta.IsStructure) {
                hammer.gameObject.SetActive(true);
            }
        }

        public override void Interact() {
            if (Target.Meta.IsStructure && Target.Resource.IsFull) {
                entity.ClearTarget();
            }
            base.Interact();
        }

        public override void InteractionCallback() {
            if (Target) {

                if (Target.Meta.IsStructure && Target.Owner.SameOwner(entity)) {

                    if (Target.State.Current.GetType() == typeof(ConstructionState)) {
                        Target.Resource.Reduce(
                            -entity.Interaction.baseDamages * 20,
                            other: entity,
                            direction: Quaternion.LookRotation(-entity.HitBox.box.up)
                        );
                    }
                }

                else if (Target.Meta.IsCollectible) {
                    var reduction = Target.Resource.Reduce(
                        Damages,
                        other: entity,
                        direction: Quaternion.LookRotation(
                            -entity.HitBox.box.up
                        )
                    );

                    if (reduction) {
                        entity.Carry.Load(Damages);
                    }
                }
            }
        }

        private void EmptyHand() {
            pick.gameObject.SetActive(false);
            axe.gameObject.SetActive(false);
            hammer.gameObject.SetActive(false);
        }
    }
}
