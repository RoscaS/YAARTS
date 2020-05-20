using System.Linq;
using Game.Entities;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    [Header("Position")]
    public float smoothing = 2;

    public float normalSpeed = 1f;
    public float fastSpeed = 1.3f;

    [Header("Rotation")]
    public float rotationSpeed = 3;


    [Header("Zoom")]
    public float zoomSpeed = 1;

    public float minZoom = 5f;
    public float maxZoom = 80f;


    private float PositionSpeed;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 newZoom;

    private Vector3 dragStartPosition;
    private Vector3 dragCurrentPosition;


    private Camera cam;
    private Transform camTransform;
    private Transform followTransform;

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private float Duration => (Time.deltaTime * smoothing) / Time.timeScale;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Start() {
        cam = GetComponentInChildren<Camera>();
        camTransform = cam.transform;

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = camTransform.localPosition;
    }

    private void Update() {
        MouseActions();
        KeyboardActions();
        FollowEntity();
        Updates();
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private void Updates() {
        CheckBoundaries();

        transform.position = Vector3.Lerp(transform.position, newPosition, Duration);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Duration);
        camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, newZoom, Duration);
    }

    private void CheckBoundaries() {
        newZoom = newZoom.y < minZoom ? new Vector3(0, minZoom, -minZoom) : newZoom;
        newZoom = newZoom.y > maxZoom ? new Vector3(0, maxZoom, -maxZoom) : newZoom;
    }

    private void FollowEntity() {
        if (followTransform != null) {
            newPosition = followTransform.position;
        }
    }

    /*------------------------------------------------------------------*\
    |*							MOUSE INPUTS
    \*------------------------------------------------------------------*/

    private void MouseActions() {
        MousePositionUpdate();

        MouseZoomUpdate();
    }

    private void MousePositionUpdate() {
        if (Input.GetMouseButtonDown(2)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float hit;

            if (plane.Raycast(ray, out hit)) {
                dragStartPosition = ray.GetPoint(hit);
            }
        }

        if (Input.GetMouseButton(2)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            float hit;

            if (plane.Raycast(ray, out hit)) {
                dragCurrentPosition = ray.GetPoint(hit);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                followTransform = null;
            }
        }
    }

    private void MouseZoomUpdate() {
        if (Input.mouseScrollDelta.y != 0) {
            newZoom += Input.mouseScrollDelta.y * new Vector3(0, -zoomSpeed, zoomSpeed);
        }
    }

    /*------------------------------------------------------------------*\
    |*							KEYBOARD INPUTS
    \*------------------------------------------------------------------*/

    private void KeyboardActions() {
        MetaKeys();
        KeyboardPositionUpdate();
        KeyboardRotationUpdate();
        KeyboardZoomUpdate();
        KeyboardFollowUpdate();
    }

    private void MetaKeys() {
        PositionSpeed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : normalSpeed;
    }

    private void KeyboardPositionUpdate() {

        if (Input.GetKey(KeyCode.W)) {
            newPosition += (transform.forward * PositionSpeed);
            followTransform = null;
        }

        if (Input.GetKey(KeyCode.S)) {
            newPosition += (transform.forward * -PositionSpeed);
            followTransform = null;
        }

        if (Input.GetKey(KeyCode.D)) {
            newPosition += (transform.right * PositionSpeed);
            followTransform = null;
        }

        if (Input.GetKey(KeyCode.A)) {
            newPosition += (transform.right * -PositionSpeed);
            followTransform = null;
        }
    }

    private void KeyboardZoomUpdate() {
        if (Input.GetKey(KeyCode.Equals)) {
            newZoom += new Vector3(0, -zoomSpeed, zoomSpeed);
        }

        if (Input.GetKey(KeyCode.Minus)) {
            newZoom -= new Vector3(0, -zoomSpeed, zoomSpeed);
        }
    }

    private void KeyboardRotationUpdate() {
        if (Input.GetKey(KeyCode.Q)) {
            newRotation *= Quaternion.Euler(Vector3.up * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.E)) {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationSpeed);
        }
    }

    private void KeyboardFollowUpdate() {
        if (Input.GetKey(KeyCode.F) && Selection.Instance.Selected.Count == 1) {
            followTransform = Selection.Instance.Selected.First().transform;
        }

    }
}
