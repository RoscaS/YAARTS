using Components;
using DefaultNamespace;
using Game.Controllers;
using UnityEngine;
using Utils;

namespace Game.Factories
{
    public class BuildingSpawner : MonoBehaviour
    {
        private const string INITIALIZATION_GAME_OBJECT = "EntitiesInstances";
        private Transform InitializationGameObject;

        public static BuildingSpawner Instance;

        private bool isEnabled;
        private bool canInteract = true;
        private Entity current;
        private Entity currentBuilder;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Awake() {
            Instance = this;
            InitializationGameObject = GameObject.Find(INITIALIZATION_GAME_OBJECT).transform;
        }

        public void Update() {

            if (isEnabled) {
                var position = Helpers.SetY(Helpers.MouseWorldPosition(), 0);
                MouseListener.Instance.dragging = false;

                current.transform.position = position;
                current.gameObject.layer = LayerMask.NameToLayer("Disabled");

                if (canInteract && Input.GetMouseButton(0)) {
                    ClickTimer();
                    CreateBuilding();
                }

                if (Input.GetKeyDown(KeyCode.Escape) ||
                    Input.GetMouseButton(1)) {
                    ToggleSpawner();
                }
            }
        }

        private void CreateBuilding() {
            var tr = current.transform;
            var path = $"Prefabs/Entities/Structures/{current.Meta.Class}";
            var go = Resources.Load(path) as GameObject;
            var instance = Instantiate(go, tr.position, tr.rotation);
            var playerType = current.Owner.playerType;
            instance.GetComponent<OwnerComponent>().playerType = playerType;
            instance.transform.parent = InitializationGameObject;
            var entity = instance.GetComponent<Entity>();



            FXFactory.Instance.CreatePuffLarge(Helpers.SetY(tr.position, .8f), 3.5f);
            FunctionDelay.Create(() => {
                currentBuilder.ClearTarget();
                currentBuilder.SetTarget(entity);
                entity.State.SetState(entity.State.states[typeof(ConstructionState)]);
                PlayerController.Instance.PayPrice(entity);
            }, .1f);

            ClearCurrent();
        }

        private void ClearCurrent() {
            current.gameObject.SetActive(false);
            current = null;
            isEnabled = false;
        }

        public void SetCurrent(Entity built, Entity builder) {
            current = built;
            currentBuilder = builder;
            current.gameObject.SetActive(true);
            isEnabled = true;

        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODS
        \*------------------------------------------------------------------*/

        private void ToggleSpawner() {
            isEnabled = !isEnabled;
        }

        private void ClickTimer() {
            canInteract = false;
            FunctionDelay.Create(() => canInteract = true, .5f);
        }
    }
}
