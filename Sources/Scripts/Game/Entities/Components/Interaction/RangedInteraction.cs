using UnityEngine;

namespace Components.Interaction
{
    public class RangedInteraction : BaseInteractionComponent
    {
        private Transform Spawn;
        public GameObject Projectile;

        protected override void Start() {
            base.Start();
            Spawn = entity.extensions.Find("ProjectileSpawn");
        }

        public override void InteractionCallback() {
            if (Target && !Target.Owner.SameOwner(entity)) {
                CreateProjectile();
            }
        }

        public void CreateProjectile() {
            var go = Instantiate(Projectile, Spawn.position, Quaternion.identity);
            var projectile = go.GetComponent<Projectile>();
            projectile.Target = entity.Interaction.Target.HitBox.box;
            projectile.Shooter = entity;
            projectile.Rb.useGravity = projectile.UseGravity;

            var velocity = ComputeVelocity(projectile);
            projectile.Rb.velocity = velocity;
            projectile.transform.rotation = Quaternion.LookRotation(velocity);
        }

        private Vector3 ComputeVelocity(Projectile projectile) {
            var origin = Spawn.position;
            var offset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0.0f, 0.45f));

            // TODO for movie only !!!
            // offset.y = .4f;


            var target = projectile.Target.position + offset;
            var speed = projectile.speed;
            var gravity = projectile.UseGravity;
            return Ballistic.ComputeInitialVelocity(origin, target, speed, gravity);
        }
    }
}
