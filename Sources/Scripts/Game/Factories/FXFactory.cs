using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

public class FXFactory
{
    private const string INITIALIZATION_GAME_OBJECT = "FXInstances";
    private readonly Transform InitializationGameObject;

    private static FXFactory instance;
    public static FXFactory Instance => instance ?? (instance = new FXFactory());

    public static Dictionary<string, GameObject> Prefabs;

    /*------------------------------------------------------------------*\
    |*							INITIALIZATION
    \*------------------------------------------------------------------*/

    private FXFactory() {
        InitializeResources();
        InitializationGameObject = GameObject.Find(INITIALIZATION_GAME_OBJECT).transform;

    }

    private void InitializeResources() {
        var fx = "Prefabs/FX";
        var particles = $"{fx}/Particles";
        var interactives = $"{fx}/Interactives";
        var stump = "Prefabs/Entities/Collectibles/Trees/Stump";
        
        Prefabs = new Dictionary<string, GameObject> {
                { "Blood", Resources.Load($"{particles}/Blood") as GameObject },
                { "ImpactDirt", Resources.Load($"{particles}/ImpactDirt") as GameObject },
                { "ImpactWood", Resources.Load($"{particles}/ImpactWood") as GameObject },
                { "ImpactStone", Resources.Load($"{particles}/ImpactStone") as GameObject },

                { "Fire", Resources.Load($"{particles}/Fire") as GameObject },
                { "Puff", Resources.Load($"{particles}/Puff") as GameObject },
                { "PuffLarge", Resources.Load($"{particles}/PuffLarge") as GameObject },

                { "Explosion", Resources.Load($"{particles}/Explosion") as GameObject },
                { "Bones", Resources.Load($"{particles}/Bones") as GameObject },

                { "Stump", Resources.Load(stump) as GameObject },
                { "Bump", Resources.Load($"{interactives}/Bump") as GameObject },
        };
    }

    /*------------------------------------------------------------------*\
    |*							IMPACTS
    \*------------------------------------------------------------------*/

    private void CreateImpact(GameObject go, Vector3 position, Quaternion direction) {
        var impact = Object.Instantiate(go, position, direction);
        impact.transform.parent = InitializationGameObject;
        FunctionDelay.Create(() => Object.Destroy(impact), 2.5f);
    }

    public void CreateBlood(Vector3 position, Quaternion direction) {
        CreateImpact(Prefabs["Blood"], position, direction);
    }

    public void CreateImpactWood(Vector3 position, Quaternion direction) {
        CreateImpact(Prefabs["ImpactWood"], position, direction);
    }

    public void CreateImpactStone(Vector3 position, Quaternion direction) {
        CreateImpact(Prefabs["ImpactStone"], position, direction);
    }

    public void CreateImpactDirt(Vector3 position) {
        CreateImpact(Prefabs["ImpactDirt"], position, Quaternion.identity);
    }

    /*------------------------------------------------------------------*\
    |*							FX
    \*------------------------------------------------------------------*/

    private GameObject CreateFX(GameObject go, Vector3 position, float scale, float duration) {
        var fx = Object.Instantiate(go, position, Quaternion.identity);
        fx.transform.localScale = Vector3.one * scale;
        fx.transform.parent = InitializationGameObject;
        FunctionDelay.Create(() => Object.Destroy(fx), duration);
        return fx;
    }

    public void CreateExplosion(Vector3 position, float scale = 3f) {
        CreateFX(Prefabs["Explosion"], position, scale, 2.5f);
    }

    public void CreatePuff(Vector3 position, float scale = 1f) {
        CreateFX(Prefabs["Puff"], position, scale, 2.5f);
    }

    public void CreatePuffLarge(Vector3 position, float scale = 1f) {
        var puff = CreateFX(Prefabs["PuffLarge"], position, scale, 2.5f);
        puff.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    public void CreateBones(Vector3 position, float scale = 2f) {
        CreateFX(Prefabs["Bones"], position, scale, 5f);
    }

    public GameObject CreateFire(Vector3 position, float scale = 1f) {
        var fx = Object.Instantiate(Prefabs["Fire"], position, Quaternion.identity);
        fx.transform.localScale = Vector3.one * scale;
        fx.transform.parent = InitializationGameObject;
        return fx;
    }

    /*------------------------------------------------------------------*\
    |*							INTERACTIVES
    \*------------------------------------------------------------------*/

    public void CreateStump(Vector3 position, Quaternion rotation) {
        position.y = -1f;
        var stump = Object.Instantiate(Prefabs["Stump"], position, rotation);
        stump.transform.parent = InitializationGameObject;
        position.y = 0;
        stump.transform.DOMove(position, 2f);
    }

    public void CreateBump(Vector3 position) {
        var action = Object.Instantiate(Prefabs["Bump"], position, Quaternion.identity);
        action.transform.parent = InitializationGameObject;
    }

}
