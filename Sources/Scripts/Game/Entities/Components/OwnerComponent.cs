using System.Collections.Generic;
using System.Linq;
using Game.Enums;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;


namespace Components
{
    public class OwnerComponent : MonoBehaviour
    {
        private static readonly string TexturesRoot = "Materials/TeamColors";

        private Entity entity;

        public PlayerType playerType;

        [HideInInspector] public string layer;
        [HideInInspector] public string targetLayer;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            entity = GetComponent<Entity>();
            if (playerType == PlayerType.Gaia) return;


            layer = IsCPU() ? PlayerType.CPU.ToString() : PlayerType.Player.ToString();

            targetLayer = entity.Meta.IsWorker
                    ? EntityType.Collectible.ToString()
                    : IsCPU() ? PlayerType.Player.ToString() : PlayerType.CPU.ToString();

            this.entity.gameObject.layer = LayerMask.NameToLayer(layer);
            SetColor();
            SetMinimapColor();
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public bool SameOwner(Entity other) => playerType == other.Owner.playerType;
        public bool IsCPU() => playerType == PlayerType.CPU;

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void SetColor() {
            var exclude = new List<string> { "Projection", "Minimap", "NamePlate" };
            var renderers = entity.GetComponentsInChildren<Renderer>()
                                  .Where(i =>  !exclude.Contains(i.name));

            var path = CreatePath();
            var mat = Resources.Load(path, typeof(Material)) as Material;
            renderers.ForEach(r => r.material = mat);
        }

        private void SetMinimapColor() {
            var renderer = entity.extensions.Find("Minimap").GetComponent<Renderer>();
            var color = IsCPU() ? "Red" : "Blue";
            var path = $"Materials/Minimap/{color}";
            renderer.material = Resources.Load(path, typeof(Material)) as Material;;
        }

        private string CreatePath() {
            var prefix = entity.Meta.Type == EntityType.Structure ? "Structures" : "Units";
            var color = IsCPU() ? "_red" : "_blue";
            var path = $"{TexturesRoot}/{prefix}/{prefix}{color}";
            return path;
        }
    }
}
