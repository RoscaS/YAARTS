using Components;
using Components.Interaction;
using Components.Resource;
using Components.State;
using DG.Tweening;
using Game.Entities;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class Entity : MonoBehaviour, IEntity
{
    public static int Count;

    public int id;
    public bool debug;
    public bool disabled;

    [HideInInspector] public Transform extensions;
    [HideInInspector] public Transform model;

    /*------------------------------------------------------------------*\
    |*							COMPONENTS
    \*------------------------------------------------------------------*/

    public MetaComponent Meta { get; set; }
    public OwnerComponent Owner { get; set; }
    public CarryComponent Carry { get; set; }
    public PriceComponent Price { get; set; }
    public BaseStateComponent State { get; set; }
    public HitBoxComponent HitBox { get; set; }
    public MovableComponent Movable { get; set; }
    public BaseResourceComponent Resource { get; set; }
    public AwarenessComponent Awareness { get; set; }
    public AnimationComponent Animation { get; set; }
    public SelectableComponent Selectable { get; set; }
    public BaseActionsComponent Actions { get; set; }
    public BaseInteractionComponent Interaction { get; set; }

    public CharacterController Controller { get; set; }
    public NavMeshAgent Agent { get; set; }
    public Collider Collider { get; set; }


    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private Selection Selection => Selection.Instance;
    private GameController GameController => GameController.Instance;

    public Vector3 Position => transform.position;
    public float Speed => Agent.velocity.magnitude;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        id = ++Count;
        extensions = transform.Find("Extensions");
        model = transform.Find("Model");

        InitializeComponents();
    }


    private void Start() {
        Collider = GetComponent<Collider>();

        if (Selectable) {
            GameController.entities.Add(this);
        }

        extensions.Find("Minimap").gameObject.SetActive(true); // todo temp
    }

    private void Update() {
        State?.Update();
        // if (disabled) return;
        // Movable?.DrawDestination();
        // Interaction?.DrawTarget();
    }

    /*------------------------------------------------------------------*\
    |*							IEntity implementation
    \*------------------------------------------------------------------*/

    public void SetDestination(Vector3 destination) {
        Movable?.SetDestination(destination);
    }

    public void SetTarget(Entity target) {
        Interaction?.SetTarget(target);
        Movable?.SetDestination(target.Position);
    }

    public void ClearDestination() {
        Movable?.ClearDestination();
    }

    public void ClearTarget() {
        Interaction?.ClearTarget();
    }

    /*------------------------------------------------------------------*\
    |*							INITIALIZATION
    \*------------------------------------------------------------------*/

    private void InitializeComponents() {
        Meta = GetComponentInChildren<MetaComponent>();
        Owner = GetComponentInChildren<OwnerComponent>();
        Price = GetComponentInChildren<PriceComponent>();
        State = GetComponentInChildren<BaseStateComponent>();
        Carry = GetComponentInChildren<CarryComponent>();
        HitBox = GetComponentInChildren<HitBoxComponent>();
        Movable = GetComponentInChildren<MovableComponent>();
        Resource = GetComponentInChildren<BaseResourceComponent>();
        Animation = GetComponentInChildren<AnimationComponent>();
        Awareness = GetComponentInChildren<AwarenessComponent>();
        Selectable = GetComponentInChildren<SelectableComponent>();
        Interaction = GetComponentInChildren<BaseInteractionComponent>();
        Actions = GetComponentInChildren<BaseActionsComponent>();

        Agent = GetComponent<NavMeshAgent>();
        Controller = GetComponent<CharacterController>();
    }

    /*------------------------------------------------------------------*\
    |*							OVERRIDE
    \*------------------------------------------------------------------*/

    public override string ToString() => name;

    /*------------------------------------------------------------------*\
    |*							HELPERS
    \*------------------------------------------------------------------*/

    public void HardLookAtTarget() {
        var deltaVec = Interaction.Target.Position - Position;
        var rotation = Quaternion.LookRotation(deltaVec);
        transform.rotation = rotation;
    }

    public void Disable(bool soft = true) {

        gameObject.layer = LayerMask.NameToLayer("Disabled");
        if (soft) Animation?.SetDeathAnimation();


        ClearTarget();
        ClearDestination();
        RemoveFromLists();
        DisableExtensions();
        disabled = true;


        DisableComponents(soft);

        if (soft) {
            GameController.RegisterKill(this);
        }
        else {
            GameController.RemoveFromEntities(this);
            FadeOut(.75f);
        }
    }

    public void FadeOut(float duration = -1, float depth = 2) {
        duration = duration == -1
                ? GameController.bodyFadeEffectDuration
                : duration;

        transform.DOMoveY(-depth, duration)
                 .OnComplete(Destroy);
    }

    public void Destroy() {
        Destroy(gameObject);
    }

    /*------------------------------------------------------------------*\
    |*							CLEANUP
    \*------------------------------------------------------------------*/

    public void DisableExtensions() {
        extensions.gameObject.SetActive(false);
    }

    private void RemoveFromLists() {
        if (Selectable && Selectable.IsSelected) Selection.Remove(this);
        GameController.RemoveFromEntities(this);
    }

    private void DisableComponents(bool soft) {
        if (State) {
            State.Current.OnExit();
        }
        // State = null;
        Selectable = null;
        Awareness = null;

        if (soft) {
            FunctionDelay.Create(DisablePhysics, .5f);
        }
        else {
            DisablePhysics();
        }
    }

    private void DisablePhysics() {
        if (Controller) Controller.enabled = false;
        if (Agent) Agent.enabled = false;
    }
}
