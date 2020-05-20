using System.Collections;
using UnityEngine;

public class Arrow : Projectile
{
    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/
    protected void FixedUpdate() {
        if (!isImpact) {
            transform.rotation = Quaternion.LookRotation(Rb.velocity);
        }
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    protected override void Hit(Collider other) {

        Entity otherEntity = other.CompareTag("Structure")
                ? other.transform.parent.GetComponent<Entity>()
                : other.GetComponent<Entity>();

        if (otherEntity != null) {

            if (!Shooter.Owner.SameOwner(otherEntity)) {

                transform.parent = otherEntity.HitBox.box;

                otherEntity.Resource.Reduce(
                    Shooter.Interaction.Damages,
                    transform.position,
                    Quaternion.LookRotation(-Rb.velocity)
                );

            }
            else {
                Destroy(this.gameObject);
                return;
            }
        }

        else {
            var position = transform.position + new Vector3(0, .1f, 0);
            StartCoroutine(DestructionDelay());
            FXFactory.Instance.CreateImpactDirt(position);
        }

        Rb.isKinematic = true;
        transform.GetComponent<Collider>().enabled = false;
    }

    private IEnumerator DestructionDelay() {
        yield return new WaitForSeconds(GameController.Instance.arrowFadeTime);
        Destroy(this.gameObject);
    }
}
