using System;
using Components;
using Game.Controllers;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Game.Factories
{
    public class CharacterSpawner : MonoBehaviour
    {
        private const string INITIALIZATION_GAME_OBJECT = "EntitiesInstances";
        private Transform InitializationGameObject;

        public static CharacterSpawner Instance;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Awake() {
            Instance = this;
            InitializationGameObject = GameObject.Find(INITIALIZATION_GAME_OBJECT).transform;
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODS
        \*------------------------------------------------------------------*/

        public void StartTraining(Entity built, Entity builder) {
            if (builder.Actions.isBUilding) return;
            var constructionTime = builder.Meta.constructionTime;
            FunctionDelay.Create(() => CreateCharacter(built, builder), constructionTime);
            builder.Actions.isBUilding = true;


            var go = builder.extensions.Find("ProgressBar");
            go.gameObject.SetActive(true);
            var bar = go.GetComponentInChildren<HealthBar>();
            bar.InitHealth(100);
            bar.SetValue(0);
            bar.FillUpDelay(constructionTime);
            FunctionDelay.Create(() => {
                go.gameObject.SetActive(false);
            }, constructionTime);


        }

        private void CreateCharacter(Entity built, Entity builder) {
            var path = $"Prefabs/Entities/Characters/{built.Meta.Class}";
            var spawn = builder.extensions.Find("Spawn").transform;
            var go = Resources.Load(path) as GameObject;

            var instance = Instantiate(go,
                Helpers.SetZ(spawn.position, spawn.position.z + Random.Range(-2, 2)),
                Helpers.RandomRotation()
            );

            var playerType = builder.Owner.playerType;
            instance.GetComponent<OwnerComponent>().playerType = playerType;
            instance.transform.parent = InitializationGameObject;

            var entity = instance.GetComponent<Entity>();

            FunctionDelay.Create(() => {
                    PlayerController.Instance.PayPrice(entity);
                    entity.SetDestination(entity.Position + Vector3.forward);
                    PlayerController.Instance.Message = $"New {built.Meta.Class} ready !";
                    PlayerController.Instance.Population++;
                    builder.Actions.isBUilding = false;

                },
                .1f
            );
        }
    }
}
