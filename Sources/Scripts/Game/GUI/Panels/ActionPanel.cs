using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Enums;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GUI.Panels
{
    public class ActionPanel : MonoBehaviour
    {
        public Transform slots;

        [Header("Panels")]
        public Transform empty;
        public Transform items;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            Selection.Instance.OnSelectionChanged += SelectionChangeHandler;
        }

        /*------------------------------------------------------------------*\
        |*							HANDLERS
        \*------------------------------------------------------------------*/

        private void InjectActions(List<Entity> entities) {
            if (entities.Any()) {
                ClearSlots();
                ReinitializePanels();
                items.gameObject.SetActive(true);

                var entity = entities.First();
                var actions = entity.Actions.actions;


                for (int i = 0; i < actions.Count; i++) {
                    var slot = slots.GetChild(i);
                    var icon = slot.Find("IconSlot").GetComponent<RawImage>();
                    var button = slot.GetComponent<Button>();

                    IGUIAction action = actions[i];

                    icon.texture = action.Built.Meta.Portrait;
                    button.onClick.AddListener(action.Callback);

                    slot.gameObject.SetActive(true);
                    icon.gameObject.SetActive(true);
                }
            }
        }

        private void SelectionChangeHandler(List<Entity> selection) {

            var workers = selection.Where(e => e.Meta.IsWorker).ToList();

            if (workers.Any()) {
                InjectActions(workers);
                return;
            }

            var archeries = selection.Where(e => e.Meta.Class == EntityClass.Archery).ToList();

            if (archeries.Any()) {
                InjectActions(archeries);
                return;
            }

            var barracks = selection.Where(e => e.Meta.Class == EntityClass.Barracks).ToList();

            if (barracks.Any()) {
                InjectActions(barracks);
                return;
            }

            var tc = selection.Where(e => e.Meta.Class == EntityClass.TownCenter).ToList();

            if (tc.Any()) {
                InjectActions(tc);
                return;
            }

            ReinitializePanels();
            empty.gameObject.SetActive(true);
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void ClearSlots() {
            for (int i = 0; i < slots.childCount - 1; i++) {
                slots.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                slots.GetChild(i).Find("IconSlot").gameObject.SetActive(false);
                slots.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void ReinitializePanels() {
            empty.gameObject.SetActive(false);
            items.gameObject.SetActive(false);
        }
    }
}
