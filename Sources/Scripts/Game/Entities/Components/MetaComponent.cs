using Components.Actions;
using Game.Enums;
using Game.Interfaces;
using UnityEngine;

namespace Components
{
    public class MetaComponent : MonoBehaviour
    {
        private Entity entity;

        public Texture2D Portrait;
        public EntityType Type;
        public EntityClass Class;
        public float constructionTime = 5f;

        private void Start() {
            entity = GetComponent<Entity>();
            entity.name = $"{Class}#{entity.id}";
        }

        public bool IsWorker => Class == EntityClass.Worker;
        public bool IsStructure => Type == EntityType.Structure;
        public bool IsCollectible => Type == EntityType.Collectible;


        public IGUIAction Build(Entity builder) {
            return new BuildAction(built: GetComponent<Entity>(), builder: builder);
        }
    }
}
