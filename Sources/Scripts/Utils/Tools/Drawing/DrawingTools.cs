using System;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

public static class DrawingTools
{
    /*------------------------------------------------------------------*\
    |*							SHAPES
    \*------------------------------------------------------------------*/


    public static void Line(Vector3 origin, Vector3 destination, Color color) {
        DebugExtension.DebugCylinder(origin, destination, color, .01f);
    }

    public static void Cylinder(Vector3 origin, Vector3 destination, Color color) {
        DebugExtension.DebugCylinder(origin, destination, color, .2f);
    }

    public static void Arrow(Vector3 origin, Vector3 direction, Color color) {
        DebugExtension.DebugArrow(origin, direction, color);
    }

    public static void Circle(Vector3 origin, float radius, Color color) {
        DebugExtension.DebugCircle(origin, new Vector3(0, 1, 0), color, radius);
    }

    public static void CircleWithLine(Vector3 origin, Vector3 destination, float radius,
                                          Color color) {
        var circleColor = color;
        Line(origin, destination, circleColor);
        circleColor.a = circleColor.a * 2;
        Circle(destination, radius, circleColor);
    }


    /*------------------------------------------------------------------*\
    |*							TEXT
    \*------------------------------------------------------------------*/

    // Creates a World Text object at the world position
    public static TextMesh Text(string text, Vector3 position) {
        return DrawText.CreateWorldText(text, position);
    }

    // Creates a World Text object at the world position
    public static TextMesh Text(string text, Transform parent, Vector3 offset) {
        return DrawText.CreateWorldText(text, offset, parent);
    }

    // Creates a World Text object at the world position
    public static TextMesh TextMouse(string text) {
        return DrawText.CreateWorldText(text, Helpers.MouseWorldPosition());
    }

    // Creates a Text pop up at the world position
    public static void TextPopup(string text, Vector3 position, float height = 2f, float time = 4f, int size = 24, Color? color = null) {
        color = color ?? Color.white;
        var target = Helpers.SetY(position, position.y + height);
        var textMesh = DrawText.CreateWorldTextPopup(text, position, target: target, time: time, fontSize: size, color: color);
        textMesh.transform.parent = GameObject.Find("TextMeshInstances").transform;
    }

    // World text pop up at mouse position
    public static void TextPopupMouse(string text, float time = 4f) {
        DrawText.CreateWorldTextPopup(text, Helpers.MouseWorldPosition(), time: time);
    }

    // Text Updater in World, (parent == null) = world position
    public static FunctionUpdater TextUpdater(Func<string> GetTextFunc, Vector3 offset, Transform parent = null) {
        return DrawText.CreateWorldTextUpdater(GetTextFunc, offset, parent);
    }


    // Text Updater always following mouse
    public static FunctionUpdater MouseTextUpdater(Func<string> GetTextFunc, Vector3 offset = default) {
        GameObject gameObject = new GameObject();

        FunctionUpdater.Create(
                () => {
                    gameObject.transform.position = Helpers.MouseWorldPosition() + offset;
                    return false;
                }
        );
        return TextUpdater(GetTextFunc, Vector3.zero, gameObject.transform);
    }

    // Trigger Action on Key
    // public static FunctionUpdater KeyCodeAction(KeyCode keyCode, Action onKeyDown) {
        // return UtilsClass.CreateKeyCodeAction(keyCode, onKeyDown);
    // }
}
