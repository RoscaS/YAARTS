using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace Components.Resource
{
    public class WoodPointsResource : BaseResourceComponent
    {
        public static readonly float angle = 90f;
        public static readonly float fallDuration = 5f;

        private ParticleSystem particles;

        public override string ResourceLabel { get; } = "WOOD";

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/


        protected override void Start() {
            base.Start();
            particles = entity.extensions
                              .Find("Particles")
                              .Find("FallingLeaves")
                              .GetComponent<ParticleSystem>();
        }

        /*------------------------------------------------------------------*\
        |*					    BaseResource Implementation
        \*------------------------------------------------------------------*/


        protected override void PlayFXs(Vector3 position, Quaternion direction, Entity other) {
            particles.Play();
            WobbleAction();
            if (!GameController.Instance.particlesParticles) return;

            if (other != null) {
                position = Helpers.SetY(other.Interaction.closestPoint, 1.2f);
            }
            FXFactory.Instance.CreateImpactWood(position, direction);
        }

        protected override void Terminate() {
            if (entity.disabled) return;
            entity.disabled = true;
            entity.DisableExtensions();
            entity.gameObject.layer = LayerMask.NameToLayer("Disabled");
            FallTween();
        }
        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/



        private void WobbleAction() {
            entity.transform.DOShakeRotation(.5f, 1f);
        }

        private void FallTween() {
            var tr = entity.transform;
            var position = tr.position;
            var rotation = tr.rotation;
            var direction = rotation.eulerAngles;
            entity.disabled = true;

            if (Random.Range(0, 1) == 0) {
                direction.x += angle;
            }
            else {
                direction.z += angle;
            }

            var fall = tr.DOLocalRotate(direction, fallDuration)
                         .SetEase(Ease.InQuint)
                         .OnPlay(() => FXFactory.Instance.CreateStump(position, rotation));

            var kill = tr.DOMoveY(-2, 5f)
                         .SetEase(Ease.InQuint)
                         .OnComplete(() => entity.Disable(soft: false));

            DOTween.Sequence().Append(fall).AppendInterval(2).Append(kill);
        }
    }
}
