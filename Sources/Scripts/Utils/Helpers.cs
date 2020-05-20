using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    // Implement Lazy loading for MouseWorld position to avoid multiple computation per frame.

    public static Vector3 MouseWorldPosition() =>
            MouseWorldPosition(Input.mousePosition, Camera.main);

    public static Vector3 MouseWorldPosition(Vector3 position, Camera camera) {
        Physics.Raycast(camera.ScreenPointToRay(position), out var hit, 500f);
        return hit.point;
    }

    public static void Bilboard(Transform transform) => Bilboard(transform, Camera.main);

    public static void Bilboard(Transform transform, Camera camera) {
        transform.LookAt(transform.position + camera.transform.forward);
    }

    public static Quaternion RandomRotation() {
        return Quaternion.Euler(0, Random.Range(0, 361), 0);
    }

    public static Vector3 Random2DDirection() {
        var point3D = Random3DDirection();
        point3D.y = 0;
        return point3D;
    }

    public static Vector3 Random3DDirection() {
        return Random.insideUnitSphere.normalized;
    }


    public static Vector3 ApplyRotationToVector(Vector3 vec, Vector3 vecRotation) {
        vecRotation = vecRotation.normalized;
        float n = Mathf.Atan2(vecRotation.y, vecRotation.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return ApplyRotationToVector(vec, n);
    }

    public static Vector3 ApplyRotationToVector(Vector3 vec, float angle) {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

    public static Quaternion CameraFacingRotation() {
        var quaternion = Quaternion.identity;
        quaternion.SetLookRotation(-Camera.main.transform.forward);
        return quaternion;
    }

    public static Vector3 SetX(Vector3 vector, float x) {
        vector.x = x;
        return vector;
    }

    public static Vector3 SetY(Vector3 vector, float y) {
        vector.y = y;
        return vector;
    }

    public static Vector3 SetZ(Vector3 vector, float z) {
        vector.z = z;
        return vector;
    }

    public static Mesh PolyMesh(float radius, int n) {
        Mesh mesh = new Mesh();

        List<Vector3> verticiesList = new List<Vector3>();
        List<int> trianglesList = new List<int>();

        for (int i = 0; i < n; i++) {
            var x = radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            var z = radius * Mathf.Cos((2 * Mathf.PI * i) / n);
            verticiesList.Add(new Vector3(x, 0f, z));
        }

        for (int i = 0; i < (n - 2); i++) {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }

        mesh.vertices = verticiesList.ToArray();
        mesh.triangles = trianglesList.ToArray();

        return mesh;
    }

}
