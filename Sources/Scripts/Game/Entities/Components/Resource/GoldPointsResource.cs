using UnityEngine;


namespace Components.Resource
{
    public class GoldPointsResource : BaseResourceComponent
    {
        public static readonly float angle = 90f;

        public override string ResourceLabel { get; } = "GOLD";

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/


        protected override void Start() {
            base.Start();
        }

        /*------------------------------------------------------------------*\
        |*					    BaseResource Implementation
        \*------------------------------------------------------------------*/

        protected override void UpdateValues(float value) {
            base.UpdateValues(value);
        }

        protected override void PlayFXs(Vector3 position, Quaternion direction, Entity other) {
            if (!GameController.Instance.particlesParticles) return;

            if (other != null) {
                position = Helpers.SetY(other.Interaction.closestPoint, 1.2f);
            }
            FXFactory.Instance.CreateImpactStone(position, direction);
        }

        protected override void Terminate() {
            if (entity.disabled) return;
            entity.disabled = true;
            entity.DisableExtensions();
            entity.gameObject.layer = LayerMask.NameToLayer("Disabled");
        }
        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/
    }
}
