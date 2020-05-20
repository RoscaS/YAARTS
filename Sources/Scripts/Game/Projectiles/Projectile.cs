using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected bool isImpact;
    protected List<int> IgnoreList = new List<int>();

    public float speed = 20f;
    public bool UseGravity = true;
    [HideInInspector] public Rigidbody Rb;
    [HideInInspector] public Entity Shooter;
    [HideInInspector] public Transform Target;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    protected void Awake() {
        Rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start() {
        if (!Shooter) return;
        IgnoreList = new List<int>
                { Shooter.gameObject.layer, LayerMask.NameToLayer("Projectile") };
    }

    protected void OnTriggerEnter(Collider other) {
        if (isImpact || IgnoreList.Contains(other.gameObject.layer)) return;
        isImpact = true;
        Hit(other);
    }

    protected virtual void Hit(Collider other) { }
}
