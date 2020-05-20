using System;
using System.Linq;
using Game.Enums;
using UnityEngine;
using Utils;

namespace Game.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public int wood;

        public int Wood {
            get => wood;
            set {
                wood = value;

                try {
                    OnWoodChanged.Invoke(wood);
                }
                catch (Exception) {
                    // ignored
                }

            }
        }

        public int gold;

        public int Gold {
            get => gold;
            set {
                gold = value;

                try {
                    OnGoldChanged.Invoke(gold);
                }
                catch (Exception) {
                    // ignored
                }
            }
        }

        public int population;

        public int Population {
            get => population;
            set {
                population = value;

                try {
                    OnPopulationChanged.Invoke(population);
                }
                catch (Exception) {
                    // ignored
                }
            }
        }

        private string message;

        public string Message {
            get => message;
            set {
                message = value;
                OnMessage.Invoke(message);
            }
        }

        /*------------------------------------------------------------------*\
        |*							EVENTS
        \*------------------------------------------------------------------*/

        public event Action<int> OnPopulationChanged;
        public event Action<int> OnWoodChanged;
        public event Action<int> OnGoldChanged;
        public event Action<string> OnMessage;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            Gold = gold;
            Wood = wood;
            FunctionDelay.Create(InitPopCount, .5f);
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void InitPopCount() {
            Population = GameController
                  .Instance.entities
                  .Where(i => !i.Meta.IsStructure)
                  .Count(i => i.Owner.playerType == PlayerType.Player);
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public bool CheckPrice(Entity entity) {
            var goldCheck = Gold >= entity.Price.gold;
            var woodCheck = Wood >= entity.Price.wood;

            if (goldCheck && woodCheck) {
                return true;
            }

            if (!goldCheck && !woodCheck) {
                Message = "Not enough resources !";
            }

            else if (goldCheck && !woodCheck) {
                Message = "Not enough wood !";
            }

            else {
                Message = "Not enough gold !";
            }

            return false;
        }

        public void PayPrice(Entity entity) {
            Wood -= entity.Price.wood;
            Gold -= entity.Price.gold;
        }
    }
}
