using UnityEngine;

#pragma warning disable 649

/*
 * Global Asset references
 * Edit Asset references in the prefab CM/Resources/CMAssets
 * */
public class Assets : MonoBehaviour
{
    // Internal instance reference
    private static Assets _i;

    // Instance reference
    public static Assets i { get { return _i; } }


    // All references

    public Sprite s_White;
    public Sprite s_Circle;

    public Material m_White;
}
