using UnityEngine;

public class Bilboard : MonoBehaviour
{
    private Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    void LateUpdate() {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
