using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Factories
{
    // TODO It's not a factory anymore find a better name
    public class EntityFactory : MonoBehaviour
    {
        private const string INITIALIZATION_GAME_OBJECT = "EntitiesModels";
        private Transform InitializationGameObject;

        public static EntityFactory Instance;

        public Entity[] workersPrefab;
        [HideInInspector] public List<Entity> workersModels = new List<Entity>();

        public Entity[] rangedPrefabs;
        [HideInInspector] public List<Entity> rangedModels = new List<Entity>();

        public Entity[] meleePrefabs;
        [HideInInspector] public List<Entity> meleeModels = new List<Entity>();

        public Entity[] buildingsPrefabs;
        [HideInInspector] public List<Entity> buildingsModels = new List<Entity>();


        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Awake() {
            Instance = this;

            InitializationGameObject = GameObject.Find(INITIALIZATION_GAME_OBJECT).transform;
            InitializeModels(buildingsPrefabs, buildingsModels);
            InitializeModels(rangedPrefabs, rangedModels);
            InitializeModels(workersPrefab, workersModels);
            InitializeModels(meleePrefabs, meleeModels);
        }

        private void InitializeModels(Entity[] prefabs, List<Entity> container) {
            foreach (var prefab in prefabs) {
                var entity = Instantiate(prefab).GetComponent<Entity>();

                entity.disabled = true;
                entity.enabled = false;


                if (entity.Meta.IsStructure) {
                    entity.GetComponent<NavMeshObstacle>().enabled = false;
                }
                else {
                    entity.GetComponent<NavMeshAgent>().enabled = false;
                }

                entity.gameObject.SetActive(false);
                entity.extensions.gameObject.SetActive(false);
                entity.transform.Find("HitBox")?.gameObject.SetActive(false);

                entity.transform.parent = InitializationGameObject;
                container.Add(entity);
            }
        }
    }
}
