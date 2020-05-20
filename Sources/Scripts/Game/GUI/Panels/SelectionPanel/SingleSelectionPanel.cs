using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GUI
{
    public class SingleSelectionPanel : MonoBehaviour
    {
        private Entity entity;
        private bool isActive;

        [Header("Panels")]
        public Transform empty;

        public Transform item;
        public Transform items;

        [Header("Sections")]
        public Transform attackSection;

        public Transform movementSection;
        public Transform rangeSection;
        public Transform resourceSection;
        public Transform stateSection;


        [Header("Static slots")]
        public RawImage portrait;

        public Text classType;
        public Text id;
        public Text owner;
        public Text damages;
        public Text attackSpeed;
        public Text dps;
        public Text speed;
        public Text los;
        public Text range;
        public Text resourceLabel;

        [Header("Dynamic slots")]
        public Text resource;

        public Text resourcePercent;
        public Text state;
        public Text target;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            Selection.Instance.OnSelectionChanged += SelectionChangeHandler;
        }

        private void OnGUI() {
            // TODO add dirty flag to avoid useless GUI refresh
            if (isActive) {
                BindDynamicData();
            }
        }

        /*------------------------------------------------------------------*\
        |*							HANDLERS
        \*------------------------------------------------------------------*/

        private void SelectionChangeHandler(List<Entity> selection) {
            if (selection.Count == 1) {
                entity = selection.First();
                isActive = true;

                ReinitializeElements();
                item.gameObject.SetActive(true);

                BindStaticData();
                BindDynamicData();
            }
            else {
                isActive = false;
                item.gameObject.SetActive(false);
                // empty.gameObject.SetActive(true);
            }

        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void BindStaticData() {
            if (entity.Resource) {
                resourceSection.gameObject.SetActive(true);
                resourceLabel.text = $"{entity.Resource.ResourceLabel}";
            }

            if (entity.Awareness) {
                rangeSection.gameObject.SetActive(true);
                los.text = $"{entity.Awareness.ligneOfSight}";
            }

            if (entity.Movable) {
                movementSection.gameObject.SetActive(true);
                speed.text = $"{entity.Movable.Speed}";
            }

            if (entity.Interaction) {
                attackSection.gameObject.SetActive(true);
                range.transform.parent.gameObject.SetActive(true);
                damages.text = $"{entity.Interaction.Damages}";
                attackSpeed.text = $"{entity.Interaction.CoolDown}";
                dps.text = $"{entity.Interaction.DPS}";
                range.text = $"{entity.Interaction.Range}";
            }

            portrait.texture = entity.Meta.Portrait;
            id.text = $"#{entity.id:0000}";
            classType.text = $"{entity.Meta.Class}";
            BindOwnerRelatedData();
        }

        private void BindDynamicData() {
            resource.text = entity.Resource.ToString();
            resourcePercent.text = $"{entity.Resource.Percent:00}%";
            BindStateRelatedData();
        }

        private void BindOwnerRelatedData() {
            var playerType = entity.Owner.playerType;
            owner.text = $"{playerType}".ToUpper();
            owner.color = playerType == PlayerType.CPU ? Colors.Secondary : Colors.Primary;
        }


        private void BindStateRelatedData() {
            if (!entity.State) return;

            stateSection.gameObject.SetActive(true);
            var currentState = entity.State.Current != null ? entity.State.Current.ToString() : "";
            string text = "";

            switch (currentState) {
                case "Moving":
                    var destination = (Vector3) entity.Movable.Destination;
                    text = $"({destination.x:0.00}; {destination.y:0.00})";
                    break;
                case "Engaging":
                    if (entity.Interaction.Target) {
                        text = $"Target: {entity.Interaction.Target.name}";
                    }
                    break;
                case "Engaged":
                    var interaction = entity.Interaction;
                    text = interaction.Target ? $"Target: {interaction.Target.name}" : "";
                    break;
                default:
                    text = "";
                    break;
            }

            state.text = currentState;
            target.text = text;
        }

        private void ReinitializeElements() {
            empty.gameObject.SetActive(false);
            items.gameObject.SetActive(false);
            item.gameObject.SetActive(false);

            rangeSection.gameObject.SetActive(false);
            stateSection.gameObject.SetActive(false);
            attackSection.gameObject.SetActive(false);
            resourceSection.gameObject.SetActive(false);
            movementSection.gameObject.SetActive(false);

            range.transform.parent.gameObject.SetActive(false);
        }
    }
}
