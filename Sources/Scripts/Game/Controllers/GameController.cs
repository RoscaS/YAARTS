using System.Collections.Generic;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Particles")]
    public bool particlesParticles = true;
    public bool bloodMeshParticles = true;

    [Header("Mesh")]
    public int lignOfSightPolygonSize = 32;
    public float bloodMeshRatio = 1.0f;

    [Header("Debris")]
    public int maxBodyCount = 100;
    public int arrowFadeTime = 30;
    public int bodyFadeEffectDuration = 10;

    [Header("Game")]
    public int maxUnits = 200;


    public HashSet<Entity> entities = new HashSet<Entity>();
    public Queue<Entity> killed = new Queue<Entity>();

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private KeysManager KeysManager => KeysManager.Instance;
    private MouseListener MouseListener => MouseListener.Instance;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        KeysManager.Update();
        MouseListener.Update();

    }

    /*------------------------------------------------------------------*\
    |*							PUBLIC METHODES
    \*------------------------------------------------------------------*/

    public void RegisterKill(Entity entity) {
        if (killed.Count >= maxBodyCount) {
            var e = killed.Dequeue();
            RemoveFromEntities(e);
            e.FadeOut();
        }
        killed.Enqueue(entity);
    }

    public void RemoveFromEntities(Entity entity) {
        entities.Remove(entity);
    }

    public void DisableEntities() {
        entities.ForEach(i => i.Disable(soft: false));
    }
}
