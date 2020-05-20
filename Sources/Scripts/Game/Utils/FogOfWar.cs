using UnityEngine;
using Utils;

public class FogOfWar : MonoBehaviour
{
    private int counter;

    private void Start() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }

        FunctionDelay.Create(() => {
                transform.Find("Mask").transform.position = new Vector3(0, -5, 0);
            }, .5f
        );
    }
}
