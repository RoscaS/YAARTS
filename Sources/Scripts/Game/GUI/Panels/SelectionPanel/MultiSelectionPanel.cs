using System.Collections.Generic;
using Game.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GUI
{
    public class MultiSelectionPanel : MonoBehaviour
    {
        public Transform slots;
        public Text countSlot;
        public Text overflow;

        [Header("Panels")]
        public Transform empty;
        public Transform item;
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

        private void SelectionChangeHandler(List<Entity> selection) {

            if (selection.Count > 1) {
                ClearSlots();
                ReinitializePanels();
                items.gameObject.SetActive(true);

                BindCount(selection);
                AssignSlots(selection);
            }
            else if (selection.Count == 0) {
                ReinitializePanels();
                empty.gameObject.SetActive(true);
            }

        }


        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void BindCount(List<Entity> selection) {
            countSlot.text = (selection.Count - 1).ToString();
        }

        private void AssignSlots(List<Entity> selection) {

            selection.Reverse();

            for (int i = 0; i < selection.Count; i++) {
                if (i < slots.childCount - 1) {
                    var slot = slots.GetChild(i);
                    var button = slot.GetComponent<Button>();
                    var portrait = slot.Find("IconSlot").GetComponent<RawImage>();
                    var entity = selection[i];

                    button.onClick.AddListener(() => Selection.Instance.Add(entity));
                    portrait.texture = selection[i].Meta.Portrait;
                    portrait.gameObject.SetActive(true);
                }
            }

            var delta = selection.Count - slots.childCount;

            if (delta > 0) {
                overflow.gameObject.SetActive(true);
                overflow.text = $"+ {delta}";
            }
            else {
                overflow.gameObject.SetActive(false);
            }
        }

        private void ClearSlots() {
            for (int i = 0; i < slots.childCount - 1; i++) {
                slots.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                slots.GetChild(i).Find("IconSlot").gameObject.SetActive(false);
            }
        }

        private void ReinitializePanels() {
            empty.gameObject.SetActive(false);
            items.gameObject.SetActive(false);
            item.gameObject.SetActive(false);
        }
    }
}
