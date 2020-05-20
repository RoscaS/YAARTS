using System;
using Game.Controllers;
using Game.Enums;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;


namespace Components.Resource
{
    public class HealthPointsResource : BaseResourceComponent
    {
        public override string ResourceLabel { get; } = "HEALTH";

        /*------------------------------------------------------------------*\
        |*					    BaseResource Implementation
        \*------------------------------------------------------------------*/

        protected override void UpdateValues(float value) {
            base.UpdateValues(value);
            entity.Movable.Stop();
            entity.Animation.SetDamagesAnimation();
        }

        protected override void PlayFXs(Vector3 position, Quaternion direction, Entity other) {
            if (!GameController.Instance.particlesParticles) return;
            Bleed(position, direction);
        }

        protected override void Terminate() {
            if (entity.disabled) return;
            entity.Disable();

            if (entity.Owner.playerType == PlayerType.Player) {
                PlayerController.Instance.Population--;
            }
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void Bleed(Vector3 position, Quaternion direction) {
            BloodParticlesHandler(position, direction);
            BloodMeshParticlesHandler(direction);
        }

        private void BloodParticlesHandler(Vector3 position, Quaternion direction) {
            if (position == default) {
                position = entity.HitBox.box.position;
                position.y += .45f;
            }
            FXFactory.Instance.CreateBlood(position, direction);
        }

        private void BloodMeshParticlesHandler(Quaternion direction) {
            if (GameController.Instance.bloodMeshParticles) {
                FunctionInterval.Create(CreateMeshParticles(5, 7, direction), .2f, Random.Range(3, 4));
            }
        }

        private Action CreateMeshParticles(float distance, int particles, Quaternion direction) {
            return () => {
                if (!entity) return;
                BloodParticleManager.Instance.SpawnBlood(
                    entity.Position,
                    direction * Vector3.forward,
                    distance,
                    particles
                );
            };
        }
    }
}
