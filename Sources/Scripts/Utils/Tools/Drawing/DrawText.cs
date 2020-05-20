using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Object = UnityEngine.Object;


public static class DrawText
{
    public const int sortingOrderDefault = 5000;


    // Creates a Text Mesh in the World and constantly updates it
    public static FunctionUpdater CreateWorldTextUpdater(Func<string> GetTextFunc,
                                                         Vector3 position,
                                                         Transform parent = null) {
        TextMesh textMesh = CreateWorldText(GetTextFunc(), position, parent);

        return FunctionUpdater.Create(
            () => {
                textMesh.text = GetTextFunc();
                Helpers.Bilboard(textMesh.transform);
                return false;
            },
            "WorldTextUpdater"
        );
    }


    // Create Text in the World
    public static TextMesh CreateWorldText(string text,
                                           Vector3 position = default,
                                           Transform parent = null,
                                           int fontSize = 24,
                                           Color? color = null,
                                           TextAnchor textAnchor = TextAnchor.MiddleCenter,
                                           TextAlignment textAlignment = TextAlignment.Left,
                                           int sortingOrder = sortingOrderDefault) {
        GameObject go = new GameObject("World_Text", typeof(TextMesh));

        Transform transform = go.transform;
        transform.SetParent(parent, false);
        transform.localPosition = position;
        transform.localScale = new Vector3(.2f, .2f, .2f);
        transform.gameObject.layer = LayerMask.NameToLayer("WorldText");
        Helpers.Bilboard(transform);

        TextMesh textMesh = go.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color ?? Color.white;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }

    // Create a Text Popup in the World
    public static TextMesh CreateWorldTextPopup(string text,
                                                Vector3 position = default,
                                                Transform parent = null,
                                                Vector3 target = default,
                                                int fontSize = 24,
                                                Color? color = null,
                                                float time = 4f) {
        target = target == default ? position + new Vector3(0, 5) : target;
        TextMesh textMesh = CreateWorldText(text, position, parent, fontSize, color);
        Transform transform = textMesh.transform;
        Vector3 moveAmount = (target - position) / time;

        FunctionUpdater.Create(
            () => {
                transform.position += moveAmount * Time.deltaTime;
                time -= Time.deltaTime;
                if (!(time <= 0f)) return false;
                Object.Destroy(transform.gameObject);
                return true;
            },
            "WorldTextPopup"
        );
        return textMesh;
    }
}
