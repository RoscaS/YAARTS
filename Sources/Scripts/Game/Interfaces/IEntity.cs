using UnityEngine;

    public interface IEntity
    {
        void SetDestination(Vector3 position);
        void SetTarget(Entity target);
        void ClearDestination();
        void ClearTarget();
    }
