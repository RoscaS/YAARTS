using System;
using UnityEngine;


public class MouseListener
{
    // private static readonly MouseListener instance = new MouseListener();

    private static MouseListener instance;
    public static MouseListener Instance => instance ?? (instance = new MouseListener());

    public float dragSensibility = 10f;
    public bool dragging;
    public Vector3 start;


    /*------------------------------------------------------------------*\
    |*							CONSTRUCTORS
    \*------------------------------------------------------------------*/

    private MouseListener() { }

    /*------------------------------------------------------------------*\
    |*							EVENTS
    \*------------------------------------------------------------------*/

    public event Action OnClickLeft;
    public event Action OnClickRight;
    public event Action OnDrag;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            InitDragging();
            OnClickLeft?.Invoke();
        }

        if (Input.GetMouseButtonDown(1)) {
            OnClickRight?.Invoke();
        }

        if (Input.GetMouseButtonUp(0)) {
            dragging = false;
        }

        if (dragging && Vector3.Distance(start, Input.mousePosition) > dragSensibility) {
            OnDrag?.Invoke();
        }
    }

    private void InitDragging() {
        if (!dragging) {
            dragging = true;
            start = Input.mousePosition;
        }
    }
}
