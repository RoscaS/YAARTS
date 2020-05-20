using Game.Controllers;
using Game.Enums;
using UnityEngine;
using Utils;

namespace Components
{
    public class UnloadArea : MonoBehaviour
    {
        private Entity entity;
        private float wood;
        private float gold;

        /*------------------------------------------------------------------*\
        |*							PROPS
        \*------------------------------------------------------------------*/

        PlayerController PlayerController => PlayerController.Instance;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            entity = transform.parent.parent.GetComponent<Entity>();
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        protected void OnTriggerEnter(Collider other) {
            var otherEntity = other.GetComponent<Entity>();

            if (otherEntity && otherEntity.Meta.IsWorker && otherEntity.Owner.SameOwner(entity)) {
                if (otherEntity.Carry.current > 0 ) {



                    var value = otherEntity.Carry.UnLoad();

                    if (otherEntity.Carry.collectedType == CollectibleType.Wood) {
                        PlayerController.Wood += (int) value;

                    }
                    else if (otherEntity.Carry.collectedType == CollectibleType.Gold) {
                        PlayerController.Gold += (int) value;

                    }
                    CreatePopup(value, otherEntity.Carry.collectedType);

                    if (otherEntity.Interaction.Target == entity) {

                        otherEntity.ClearTarget();
                        otherEntity.SetDestination(otherEntity.Position);
                        otherEntity.Carry.GoBackToWork();
                    }
                }
            }
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void CreatePopup(float quantity, CollectibleType type) {
            var text = $"+ {quantity} {type.ToString()}";
            var position = Helpers.SetY(entity.Position, Random.Range(2, 12));
            DrawingTools.TextPopup(text, position, 4f, 2f, 34, Colors.Primary);
        }
    }
}
