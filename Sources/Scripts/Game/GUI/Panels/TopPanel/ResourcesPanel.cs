using System.Linq;
using Game.Controllers;
using Game.Enums;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game.GUI.Panels.TopPanel
{
    public class ResourcesPanel : MonoBehaviour
    {
        public Text goldSlot;
        public Text woodSlot;
        public Text popSlot;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            PlayerController.Instance.OnGoldChanged += GoldChangedHandler;
            PlayerController.Instance.OnWoodChanged += WoodChangedHandler;
            PlayerController.Instance.OnPopulationChanged += PopulationChangedHandler;
        }




        /*------------------------------------------------------------------*\
        |*							HANDLERS
        \*------------------------------------------------------------------*/

        private void GoldChangedHandler(int value) {
            goldSlot.text = value.ToString();
        }

        private void WoodChangedHandler(int value) {
            woodSlot.text = value.ToString();
        }

        private void PopulationChangedHandler(int value) {
            popSlot.text = value.ToString();
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/
    }
}
