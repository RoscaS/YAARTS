using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputsManager
{
    private static readonly InputsManager instance = new InputsManager();
    private RaycastHit hit;

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public static InputsManager Instance => instance;
    private Camera cam => Camera.main;

    /*------------------------------------------------------------------*\
    |*							CONSTRUCTORS
    \*------------------------------------------------------------------*/

    static InputsManager() { }

    private InputsManager() {
        SubscribeToMouseListeners();
    }

    /*------------------------------------------------------------------*\
    |*							SUBSCRIPTIONS
    \*------------------------------------------------------------------*/

    private void SubscribeToMouseListeners() {
        MouseListener.Instance.OnClickLeft += ClickLeftHandler;
        MouseListener.Instance.OnClickRight += ClickRightHandler;
        MouseListener.Instance.OnDrag += DragLeftHandler;
    }

    /*------------------------------------------------------------------*\
    |*							EVENTS
    \*------------------------------------------------------------------*/

    public event Action<Entity> OnEntityLeftClicked;
    public void EntityLeftClickedEvent(Entity entityOld) => OnEntityLeftClicked?.Invoke(entityOld);

    public event Action<Entity> OnEntityRightClicked;
    public void EntityRightClickedEvent(Entity entityOld) => OnEntityRightClicked?.Invoke(entityOld);

    public event Action<Vector3> OnGroundLeftClicked;
    public void GroundLeftClickedEvent(Vector3 position) => OnGroundLeftClicked?.Invoke(position);

    public event Action<Vector3> OnGroundRightClicked;
    public void GroundRightClickedEvent(Vector3 position) => OnGroundRightClicked?.Invoke(position);

    public event Action<Bounds> OnRectangleSelection;
    public void RectangleSelectionEvent(Bounds rectangle) => OnRectangleSelection?.Invoke(rectangle);

    /*------------------------------------------------------------------*\
    |*							HANDLERS
    \*------------------------------------------------------------------*/

    private void ClickLeftHandler() => ClickHandler(0);
    private void ClickRightHandler() => ClickHandler(1);
    private void DragLeftHandler() => RectangleSelectionEvent(GetRectangleBounds());

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private void ClickHandler(int button) {
        // Click trough UI disabled:
        if (EventSystem.current.IsPointerOverGameObject()) return;

        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 500f)) {
            var selectable = hit.transform.GetComponent<Entity>();
            if (selectable != null) {

                if (!selectable.disabled) {
                    if (button == 0) EntityLeftClickedEvent(selectable);
                    else EntityRightClickedEvent(selectable);
                }
            }
            else {
                if (button == 0) GroundLeftClickedEvent(hit.point);
                else GroundRightClickedEvent(hit.point);
            }
        }
    }

    private Bounds GetRectangleBounds() {
        Vector3 v1 = cam.ScreenToViewportPoint(MouseListener.Instance.start);
        Vector3 v2 = cam.ScreenToViewportPoint(Input.mousePosition);
        Vector3 min = Vector3.Min(v1, v2);
        Vector3 max = Vector3.Max(v1, v2);
        min.z = cam.nearClipPlane;
        max.z = cam.farClipPlane;

        Bounds bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}
