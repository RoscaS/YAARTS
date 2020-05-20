using UnityEngine;

namespace Game.Utils
{
    public class LOSMesh : MonoBehaviour
    {

        private const string INITIALIZATION_GAME_OBJECT = "LOS";


        public Entity Entity { get; set; }

        private void Start() {

            GetComponent<MeshFilter>().mesh = Helpers.PolyMesh(
                Entity.Awareness.ligneOfSight,
                GameController.Instance.lignOfSightPolygonSize
            );
            transform.parent = GameObject.Find(INITIALIZATION_GAME_OBJECT).transform;
        }

        private void LateUpdate() {
            if (Entity.disabled) Destroy(this.gameObject);
            transform.position = Entity.Position;
        }
    }
}
