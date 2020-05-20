using UnityEngine;
using UnityEditor;


public class SceneViewRotations : MonoBehaviour
{
    private static readonly Quaternion TopPosition = Quaternion.Euler(90, 0, 0);
    private static readonly Quaternion BottomPosition = Quaternion.Euler(-90, 0, 0);
    private static readonly Quaternion LeftPosition = Quaternion.Euler(0, 90, 0);
    private static readonly Quaternion RightPosition = Quaternion.Euler(0, -90, 0);
    private static readonly Quaternion FrontPosition = Quaternion.Euler(0, 0, 0);
    private static readonly Quaternion BackPosition = Quaternion.Euler(0, 180, 0);

    private static readonly Quaternion[] xRotations = {FrontPosition, RightPosition, BackPosition, LeftPosition};

    private static readonly Quaternion[] yRotations = {TopPosition, BottomPosition};
    private static int xIndex;
    private static int yIndex;

    static Quaternion sPerspectiveRotation = Quaternion.Euler(0, 0, 0);

    static bool sShouldTween = true;

#if UNITY_EDITOR



    private static void StorePerspective() {
        if (SceneView.lastActiveSceneView.orthographic == false) {
            sPerspectiveRotation = SceneView.lastActiveSceneView.rotation;
        }
    }

    private static void ApplyOrthoRotation(Quaternion newRotation) {
        StorePerspective();

        SceneView.lastActiveSceneView.orthographic = true;

        if (sShouldTween) {
            SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot, newRotation);
        }
        else {
            SceneView.lastActiveSceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot,
                                                       newRotation);
        }

        SceneView.lastActiveSceneView.Repaint();
    }


    [MenuItem("Rotations/Rotate on X &w")]
    static void RotateCameraX() {
        ApplyOrthoRotation(xRotations[xIndex++]);
        if (xIndex > xRotations.Length - 1) xIndex = 0;
    }


    [MenuItem("Rotations/Rotate on Y &e")]
    static void RotateCameraY() {
        ApplyOrthoRotation(yRotations[yIndex++]);
        if (yIndex > yRotations.Length - 1) yIndex = 0;
    }

    [MenuItem("Rotations/Persp Camera &q")]
    static void PerspCamera() {
        if (SceneView.lastActiveSceneView.camera.orthographic) {
            if (sShouldTween) {
                SceneView.lastActiveSceneView.LookAt(SceneView.lastActiveSceneView.pivot,
                                                     sPerspectiveRotation);
            }
            else {
                SceneView.lastActiveSceneView.LookAtDirect(SceneView.lastActiveSceneView.pivot,
                                                           sPerspectiveRotation);
            }

            SceneView.lastActiveSceneView.orthographic = false;

            SceneView.lastActiveSceneView.Repaint();
        }
    }

#endif
}
