using UnityEngine;

namespace Components
{
    public class SelectableComponent : MonoBehaviour
    {
        [SerializeField] public bool IsSelected;

        private Entity entity;
        private Renderer projection;


        private void Start() {
            entity = GetComponent<Entity>();
            projection = entity.extensions.Find("Projection").GetComponent<Renderer>();
            SetSelected(false);
        }

        public void SetSelected(bool value) {
            if (projection) projection.enabled = value;
            IsSelected = value;
        }
    }
}
