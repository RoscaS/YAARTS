using UnityEngine;

public class FireBall : Projectile
{
    /*------------------------------------------------------------------*\
    |*						Projectile IMPLEMENTATION
    \*------------------------------------------------------------------*/

    protected override void Hit(Collider other) {
        bool structure = other.CompareTag("Structure");
        FXFactory.Instance.CreateExplosion(transform.position);
        Entity otherEntity;

        if (!structure) {
            // ActionFactory.Instance.CreateBump(Helpers.SetY(transform.position, 0));
            FXFactory.Instance.CreateBump(Helpers.SetY(transform.position, 0));
            otherEntity = other.GetComponent<Entity>();
        }
        else {
            otherEntity = other.transform.parent.GetComponent<Entity>();
        }

        if (otherEntity != null && !otherEntity.disabled) {

            if (!Shooter.Owner.SameOwner(otherEntity)) {
                if (!structure && otherEntity.Resource.Current - Shooter.Interaction.Damages <= 0) {
                    FXFactory.Instance.CreateBones(Helpers.SetY(otherEntity.Position, 1));
                    otherEntity.Disable(soft: false);
                }
                else {
                    otherEntity.Resource.Reduce(Shooter.Interaction.Damages);
                }
            }
        }
        Destroy(this.gameObject);
    }
}
