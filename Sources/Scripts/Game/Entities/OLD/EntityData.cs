using System;
using Game.Enums;
using Ludiq.PeekCore;
using UnityEngine;
using Object = UnityEngine.Object;

public class EntityData
{
    /*------------------------------------------------------------------*\
    |*							PATHS
    \*------------------------------------------------------------------*/

    private static readonly string EntitiesPath = "Prefabs/Entities";
    private static readonly string PortraitsPath = "Textures/Icons/Portraits";
    private static readonly string CharactersPath = $"{EntitiesPath}/Characters";
    private static readonly string CollectablesPath = $"{EntitiesPath}/Collectables";
    private static readonly string StructuresPath = $"{EntitiesPath}/Structures";

    /*------------------------------------------------------------------*\
    |*							META
    \*------------------------------------------------------------------*/

    public EntityClass Class;
    public EntityType Type;

    private Texture2D portrait;
    public Texture2D Portrait => portrait ? portrait : (portrait = LoadPortrait());

    private GameObject prefab;
    public GameObject Prefab => prefab ? prefab : (prefab = LoadPrefab());

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public float Health;
    public float Acceleration;
    public float MaxSpeed;
    public float StoppingDistance;
    public float Range;
    public float Damages;
    public float CoolDown;
    public float LigneOfSight;

    /*------------------------------------------------------------------*\
    |*							STRATEGIES
    \*------------------------------------------------------------------*/

    // public Func<Entity, IInteraction> InteractionStrategy;
    // public Func<Entity, IResource> ResourceStrategy;

    /*------------------------------------------------------------------*\
    |*							TOOLS
    \*------------------------------------------------------------------*/


    public Entity CreateEntity(Vector3 position = default, Quaternion rotation = default) {
        position = position != default ? position : Vector3.zero;

        rotation = rotation.Equals(default)
                ? Type != EntityType.Structure ? Helpers.RandomRotation() : default
                : Helpers.CameraFacingRotation();

        Entity entity = Object.Instantiate(Prefab, position, rotation).GetComponent<Entity>();
        // entity.Meta = new MetaComponent(entity, Class, Type, Portrait);
        entity.name = Class.ToString();

        return entity;
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private string Root() {
        switch (Type) {
            case EntityType.Character: return CharactersPath;
            case EntityType.Collectible: return CollectablesPath;
            default: return StructuresPath;
        }
    }

    private Texture2D LoadPortrait() => Resources.Load($"{PortraitsPath}/{Class}") as Texture2D;
    private GameObject LoadPrefab() => Resources.Load($"{Root()}/{Class}") as GameObject;
}
