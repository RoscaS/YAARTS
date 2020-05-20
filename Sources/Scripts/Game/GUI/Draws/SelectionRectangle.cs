using UnityEngine;

namespace Game.GUI.Draws
{
    public class SelectionRectangle : MonoBehaviour
    {

        private Texture2D rectangleTexture;

        private Vector3 mouseStartPoint;
        private bool selectionStarted;
        private bool draw;



        public Color color = Color.white;
        public float thickness = 1f;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            rectangleTexture = GetRectangleTexture();

            MouseListener.Instance.OnDrag += () => draw = true;
        }

        private void OnGUI() {
            if (!draw) return;
            if (!MouseListener.Instance.dragging) {
                draw = false;
            }
            DrawRectangleBorder();
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        private void MultiSelectionHandler() {
            if (!MouseListener.Instance.dragging) {
                draw = false;
            }
            DrawRectangleBorder();
        }

        private Texture2D GetRectangleTexture() {
            if (rectangleTexture == null) {
                rectangleTexture = new Texture2D(1, 1);
                rectangleTexture.SetPixel(0, 0, Color.white);
                rectangleTexture.Apply();
            }

            return rectangleTexture;
        }

        public void DrawRectangleBorder() {
            Rect rect = GetScreenRect();
            Rect Top = new Rect(rect.xMin, rect.yMin, rect.width, thickness);
            Rect Left = new Rect(rect.xMin, rect.yMin, thickness, rect.height);
            Rect Right = new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height);
            Rect Bottom = new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness);

            UnityEngine.GUI.color = color;
            UnityEngine.GUI.DrawTexture(Top, rectangleTexture);
            UnityEngine.GUI.DrawTexture(Left, rectangleTexture);
            UnityEngine.GUI.DrawTexture(Right, rectangleTexture);
            UnityEngine.GUI.DrawTexture(Bottom, rectangleTexture);
        }


        private Rect GetScreenRect() {
            Vector3 p1 = MouseListener.Instance.start;
            Vector3 p2 = Input.mousePosition;

            p1.y = Screen.height - p1.y;
            p2.y = Screen.height - p2.y;

            Vector3 topLeft = Vector3.Min(p1, p2);
            Vector3 bottomRight = Vector3.Max(p1, p2);
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }
    }
}
