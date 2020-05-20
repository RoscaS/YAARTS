using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Components.Resource
{
    public abstract class BaseResourceComponent : MonoBehaviour
    {
        protected Entity entity;

        [HideInInspector] public HealthBar Bar;
        [HideInInspector] public float Percent;
        [HideInInspector] public float Current;

        public float Total;

        /*------------------------------------------------------------------*\
        |*							PROPS
        \*------------------------------------------------------------------*/

        public bool IsFull => Percent >= 100;
        public bool IsEmpty => Current <= 0;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        protected virtual void Start() {
            entity = GetComponent<Entity>();

            entity.extensions
            .Find("HealthBar").gameObject
            .SetActive(true);
            Bar = entity.GetComponentInChildren<HealthBar>();
            if (Bar) Bar.InitHealth(Total);

            Current = Total;
            Percent = 100;
        }

        /*------------------------------------------------------------------*\
        |*							CORE
        \*------------------------------------------------------------------*/

        public bool Reduce(float value, Vector3 position = default, Quaternion direction = default, Entity other = null) {
            if (Current <= 0) return false;
            ApplyReduction(value, position, direction, other);
            if (Current <= 0) Terminate();

            // Normalize things for builders
            if (Current > Total) {
                Current = Total;
                Percent = 100;
            }
            return true;
        }

        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public override string ToString() {
            return $"{Current}/{Total}";
        }

        /*------------------------------------------------------------------*\
        |*							PRIVATE METHODES
        \*------------------------------------------------------------------*/

        protected void ApplyReduction(float value, Vector3 position, Quaternion direction, Entity other) {
            UpdateValues(value);
            PlayFXs(position, direction, other);
        }

        /*------------------------------------------------------------------*\
        |*							INTERFACE
        \*------------------------------------------------------------------*/

        protected virtual void UpdateValues(float value) {
            Current -= value;
            Percent = 100 * (Current / Total);
            Bar.SetValue(Current);
        }

        public abstract string ResourceLabel { get; }
        protected abstract void PlayFXs(Vector3 position, Quaternion direction, Entity other);
        protected abstract void Terminate();
    }
}
