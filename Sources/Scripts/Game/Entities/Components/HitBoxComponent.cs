using JetBrains.Annotations;
using UnityEngine;

namespace Components
{
    public class HitBoxComponent : MonoBehaviour
    {
        public Transform box;

        [CanBeNull] public Transform hand;
    }
}
