using System;
using System.Collections.Generic;
using System.Linq;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;

namespace Game.Entities
{
    public class Selection : MonoBehaviour, IEntity
    {
        public static Selection Instance { get; set; }

        [Header("Formation")]
        public float Offset = 1.5f;

        [Header("Overlays")]
        public bool DestinationOverlay = true;

        public bool TargetOverlay;

        public Color DestinationColor;
        public Color TargetColor;

        public List<Entity> Selected = new List<Entity>();

        /*------------------------------------------------------------------*\
        |*							PROPS
        \*------------------------------------------------------------------*/

        private Camera cam => Camera.main;

        /*------------------------------------------------------------------*\
        |*							EVENTS
        \*------------------------------------------------------------------*/

        public event Action<List<Entity>> OnSelectionChanged;

        public void SelectionChanged(List<Entity> selection) {
            OnSelectionChanged?.Invoke(selection);
        }

        /*------------------------------------------------------------------*\
        |*							SUBSCRIPTIONS
        \*------------------------------------------------------------------*/

        private void SubscribeToInputEvents() {
            InputsManager.Instance.OnEntityLeftClicked += EntityLeftClickedHandler;
            InputsManager.Instance.OnEntityRightClicked += EntityRightClickedHandler;
            InputsManager.Instance.OnGroundLeftClicked += GroundLeftClickedHandler;
            InputsManager.Instance.OnGroundRightClicked += GroundRightClickedHandler;
            InputsManager.Instance.OnRectangleSelection += RectangleSelectionHandler;
        }

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            SubscribeToInputEvents();
        }

        /*------------------------------------------------------------------*\
        |*							IEntity implementation
        \*------------------------------------------------------------------*/


        public void SetDestination(Vector3 position) {
            if (Selected.Count == 1) {
                Selected.First().SetDestination(position);
            }
            else {
                var offsets = Formations.Square(Selected.Count, position, Offset);
                Selected.Zip(offsets, Tuple.Create).ForEach(i => i.Item1.SetDestination(i.Item2));
            }

            if (Selected.Count > 3) {
                Selected.ForEach(e => e.Agent.stoppingDistance = 1f);
            }
        }

        public void SetTarget(Entity target) {
            Selected.ForEach(e => e.SetTarget(target));
        }

        public void ClearDestination() {
            Selected.ForEach(e => e.ClearDestination());
        }

        public void ClearTarget() {
            Selected.ForEach(e => e.ClearTarget());
        }

        /*------------------------------------------------------------------*\
        |*							HANDLERS
        \*------------------------------------------------------------------*/

        public void EntityLeftClickedHandler(Entity entity) {
            Add(entity);
        }

        public void GroundLeftClickedHandler(Vector3 position) {
            ClearSelection();
            SelectionChanged(Selected);
        }

        public void EntityRightClickedHandler(Entity entity) {
            SetTarget(entity);
        }

        public void GroundRightClickedHandler(Vector3 position) {
            SetDestination(position);
            ClearTarget();
        }

        private void RectangleSelectionHandler(Bounds bounds) {
            var point = new Func<Vector3, Vector3>(p => cam.WorldToViewportPoint(p));

            var bounded = GameController.Instance.entities
                                        .Where(s => s.Movable != null)
                                        .Where(s => bounds.Contains(point(s.transform.position)))
                                        .ToList();

            AddRange(bounded);
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public void Remove(Entity entity) {
            Selected.Remove(entity);
            entity.Selectable.SetSelected(false);
            SelectionChanged(Selected);
        }

        public void Add(Entity entity) {
            ClearSelection();

            // // TODO for movie only !!!
            // Selected.Add(entity);
            // entity.Selectable.SetSelected(true);
            // SelectionChanged(Selected);

            // Old version
            if (!entity.Owner.IsCPU()) {
                Selected.Add(entity);
                entity.Selectable.SetSelected(true);
                SelectionChanged(Selected);
            }

        }

        public void ClearSelection() {
            Selected.ForEach(e => e.Selectable.SetSelected(false));
            Selected.Clear();
        }

        public void AddRange(List<Entity> selection) {
            if (selection.Count == Selected.Count) return;

            GameController.Instance.entities.ForEach(i => i.Selectable.SetSelected(false));

            // TODO for movie only !!!
            // Selected = selection;
            // Selected.ForEach(i => i.Selectable.SetSelected(true));

            // Old version
            var filtered = selection.Where(i => !i.Owner.IsCPU());
            Selected = filtered.ToList();
            Selected.ForEach(i => i.Selectable.SetSelected(true));

            SelectionChanged(Selected);
        }
    }
}
