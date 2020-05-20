using Game.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace Components
{
    public class MovableComponent : MonoBehaviour
    {

        private Entity entity;


        [SerializeField] public float Acceleration;
        [SerializeField] public float Speed;
        [SerializeField] public float StoppingDistance;

        public Vector3? Destination;


        private float TotalDistance;

        /*------------------------------------------------------------------*\
        |*							PROPS
        \*------------------------------------------------------------------*/

        public NavMeshAgent Agent => entity.Agent;
        public float RemainingDistance => Agent.remainingDistance;
        public bool ReachedDestination => RemainingDistance == 0;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/



        private void Start() {
            entity = GetComponent<Entity>();
            Agent.speed = Speed;
            Agent.stoppingDistance = StoppingDistance;
        }


        /*------------------------------------------------------------------*\
        |*							PUBLIC METHODES
        \*------------------------------------------------------------------*/

        public void Stop() {
            Agent.speed = 0;
            Agent.acceleration = 1000;
        }

        public void Resume() {
            Agent.acceleration = Acceleration;
            Agent.speed = Speed;
        }

        public void SetDestination(Vector3 destination) {
            TotalDistance = Vector3.Distance(destination, entity.Position);
            Agent.SetDestination(destination);
            this.Destination = destination;

            Agent.stoppingDistance = TotalDistance > 2 * StoppingDistance
                    ? StoppingDistance
                    : StoppingDistance / 2;
        }

        public void ClearDestination() {
            ResetStoppingDistance();
            if (Agent.enabled) Agent.SetDestination(entity.Position);
            Destination = null;
            TotalDistance = 0;
        }

        public void ResetStoppingDistance() {
            Agent.stoppingDistance = StoppingDistance;
        }

        public void DrawDestination() {
            if (Destination != null && Selection.Instance.DestinationOverlay &&
                entity.Selectable.IsSelected) {
                EntityDebug.DrawDestination(entity);
            }
        }
    }
}
