using Components.State;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Components.Resource
{
    public class StructurePointsResource : BaseResourceComponent
    {
        public override string ResourceLabel { get; } = "HEALTH";

        private readonly int fireMaxLvl = 3;
        private Transform FireExtension;

        /*------------------------------------------------------------------*\
        |*							PROPS
        \*------------------------------------------------------------------*/

        private StructureStateComponent State => (StructureStateComponent) entity.State;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        protected override void Start() {
            base.Start();
            FireExtension = this.entity.extensions.Find("Fire").transform;
        }

        /*------------------------------------------------------------------*\
        |*					    BaseResource Implementation
        \*------------------------------------------------------------------*/

        protected override void UpdateValues(float value) {
            base.UpdateValues(value);

            if (State.completionState == 1 && entity.Resource.Percent > 50) {
                IncreaseCompletionState();
            }

            if (value > 0 && State.completed) {
                UpdateFireState();
            }
        }

        protected override void PlayFXs(Vector3 position, Quaternion direction, Entity other) {
            if (!GameController.Instance.particlesParticles) return;

            if (other != null) {
                position = Helpers.SetY(other.Interaction.closestPoint, .75f);
            }
            FXFactory.Instance.CreateImpactStone(position, direction);
        }

        protected override void Terminate() {
            if (entity.disabled) return;
            DestroyStructure();
            entity.Disable();
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void UpdateFireState() {
            var fireLevel = Mathf.Abs(fireMaxLvl - ((int) Percent * (fireMaxLvl + 1) / 100)) - 1;
            if (fireLevel < 0) return;
            var go = FireExtension.GetChild(fireLevel).gameObject;
            go.SetActive(true);
            go.transform.DOScale(1, 3f);
        }

        private void DestroyStructure() {
            FadeOutFire();
            entity.transform.Find("HitBox").gameObject.SetActive(false);
            DecreaseCompletionState(10f, 6f);
            FunctionDelay.Create(() => DecreaseCompletionState(6f, 7f), 1f);
        }

        private void FadeOutFire() {
            FireExtension.parent = entity.transform;

            foreach (Transform fire in FireExtension) {
                fire.DOMoveY(1, 2f).OnComplete(
                    () => fire.DOScale(0, 30f).OnComplete(
                        () => entity.extensions.gameObject.SetActive(false)
                    )
                );
            }
        }

        private void DecreaseCompletionState(float explosionScale, float puffScale) {
            if (State.completionState <= 1) return;
            ExplosionFX(explosionScale, puffScale);
            entity.model.Find($"state_{State.completionState}").gameObject.SetActive(false);
            entity.model.Find($"state_{--State.completionState}").gameObject.SetActive(true);
        }

        public void IncreaseCompletionState() {
            entity.model.Find($"state_{State.completionState}").gameObject.SetActive(false);
            entity.model.Find($"state_{++State.completionState}").gameObject.SetActive(true);
        }

        private void ExplosionFX(float explosionScale, float puffScale) {
            var position = Helpers.SetY(entity.Position, 2f);
            entity.transform.DOShakeRotation(.5f, .5f);
            FXFactory.Instance.CreateExplosion(position, explosionScale);
            FXFactory.Instance.CreatePuff(position, puffScale);
        }
    }
}
