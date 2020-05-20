using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeScale = 1f;
    public float maxValue = 1f;

    private float increment;
    private float normal = 0.001f;
    private float faster = 0.1f;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Start() {
        increment = normal;
    }

    private void Update() {
        Time.timeScale = timeScale;
    }


    /*------------------------------------------------------------------*\
    |*							PUBLIC METHODES
    \*------------------------------------------------------------------*/

    public void ResetTimeScale() {
        timeScale = 1;
        DrawingTools.TextPopupMouse("Initial TimeScale.", timeScale);
    }

    public void SetTimeScale(float value) {
        timeScale = value;
    }

    public void IncrementTimeScale() {
        increment = Input.GetKey(KeyCode.LeftControl) ? faster : normal;
        timeScale += increment;

        if (timeScale >= maxValue) {
            timeScale = maxValue;
            DrawingTools.TextPopupMouse("Max TimeScale.", timeScale);
        }
    }

    public void DecrementTimeScale() {
        increment = Input.GetKey(KeyCode.LeftControl) ? faster : normal;
        timeScale -= increment;

        if (timeScale <= 0.001f) {
            timeScale = 0.001f;

            DrawingTools.TextPopupMouse("Min TimeScale.", timeScale);
        }
    }
}
