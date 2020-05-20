using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MeshParticleController : MonoBehaviour
{

    public const int AvgMeshPerEntity = 100;

    public static MeshParticleController Instance { get; private set; }


    // Set in the Editor using Pixel Values
    [System.Serializable]
    public struct ParticleUVPixels
    {
        public Vector2Int uv00Pixels;
        public Vector2Int uv11Pixels;
    }

    // Holds normalized texture UV Coordinates
    private struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField] public ParticleUVPixels[] particleUVPixelsArray;
    private UVCoords[] uvCoordsArray;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    private bool updateUV;
    private bool updateVertices;
    private bool updateTriangles;

    private int currentQuadIndex;
    private int maxQuadAmount;

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private GameController GameController => GameController.Instance;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;

        var maxBodyCount = GameController.maxBodyCount;
        var bloodMeshRatio = GameController.bloodMeshRatio;
        maxQuadAmount = (int) (AvgMeshPerEntity * maxBodyCount * bloodMeshRatio);

        InitializeMesh();
        InitializeUVArray();
    }

    private void LateUpdate() {
        if (updateVertices) {
            mesh.vertices = vertices;
            updateVertices = false;
        }

        if (updateUV) {
            mesh.uv = uv;
            updateUV = false;
        }

        if (updateTriangles) {
            mesh.triangles = triangles;
            updateTriangles = false;
        }
    }

    /*------------------------------------------------------------------*\
    |*							INITIALIZATION
    \*------------------------------------------------------------------*/

    private void InitializeMesh() {
        mesh = new Mesh();
        vertices = new Vector3[4 * maxQuadAmount];
        uv = new Vector2[4 * maxQuadAmount];
        triangles = new int[6 * maxQuadAmount];
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void InitializeUVArray() {
        var material = GetComponent<MeshRenderer>().material;
        var uvCoordsList = new List<UVCoords>();
        var mainTexture = material.mainTexture;
        var textureWidth = mainTexture.width;
        var textureHeight = mainTexture.height;

        foreach (ParticleUVPixels particleUVPixels in particleUVPixelsArray) {
            UVCoords uvCoords = new UVCoords {
                    uv00 = new Vector2(
                        (float) particleUVPixels.uv00Pixels.x / textureWidth,
                        (float) particleUVPixels.uv00Pixels.y / textureHeight
                    ),
                    uv11 = new Vector2(
                        (float) particleUVPixels.uv11Pixels.x / textureWidth,
                        (float) particleUVPixels.uv11Pixels.y / textureHeight
                    ),
            };
            uvCoordsList.Add(uvCoords);

        }
        uvCoordsArray = uvCoordsList.ToArray();
    }

    /*------------------------------------------------------------------*\
    |*							PUBLIC METHODES
    \*------------------------------------------------------------------*/


    public int AddQuad(Vector3 position, float rotation, Vector3 size, bool skewed, int uvIndex) {
        currentQuadIndex++;

        if (currentQuadIndex >= maxQuadAmount) {
            currentQuadIndex = 0;
        }

        UpdateQuad(currentQuadIndex, position, rotation, size, skewed, uvIndex);

        return currentQuadIndex;
    }

    public void UpdateQuad(int idx, Vector3 position, float rotation, Vector3 size, bool skewed, int uvIndex) {
        // Relocate vertices
        int vIndex = idx * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        if (skewed) {
            vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(-size.x, -size.y);
            vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(-size.x, +size.y);
            vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(+size.x, +size.y);
            vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(+size.x, -size.y); }
        else {
            vertices[vIndex0] = position + Quaternion.Euler(90f, 0, rotation - 180) * size;
            vertices[vIndex1] = position + Quaternion.Euler(90f, 0, rotation - 270) * size;
            vertices[vIndex2] = position + Quaternion.Euler(90f, 0, rotation - 0) * size;
            vertices[vIndex3] = position + Quaternion.Euler(90f, 0, rotation - 90) * size;
        }

        // UV
        UVCoords uvCoords = uvCoordsArray[uvIndex];
        uv[vIndex0] = uvCoords.uv00;
        uv[vIndex1] = new Vector2(uvCoords.uv00.x, uvCoords.uv11.y);
        uv[vIndex2] = uvCoords.uv11;
        uv[vIndex3] = new Vector2(uvCoords.uv11.x, uvCoords.uv00.y);

        //Create triangles
        int tIndex = idx * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        updateVertices = true;
        updateUV = true;
        updateTriangles = true;
    }

    public void DestroyQuad(int quadIndex) {
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = Vector3.zero;
        vertices[vIndex1] = Vector3.zero;
        vertices[vIndex2] = Vector3.zero;
        vertices[vIndex3] = Vector3.zero;

        updateVertices = true;
    }

    public void ClearParticles() {
        for (int i = 0; i < currentQuadIndex; i++) {
            DestroyQuad(i);
        }
    }
}
