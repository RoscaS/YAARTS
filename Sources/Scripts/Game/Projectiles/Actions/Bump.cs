using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Game.Projectiles.Interactions
{
    public class Bump : MonoBehaviour
    {
        private static readonly float GrowthRate = 0.5f;
        private static readonly float GrowthSpeed = 0.005f;
        private static readonly float Size = 1.5f;
        private static readonly int Ticks = (int) (Size / GrowthRate);

        private NavMeshAgent Agent;

        private void Start() {
            Agent = GetComponent<NavMeshAgent>();
            FunctionInterval.Create(() => Agent.radius += GrowthRate, GrowthSpeed, Ticks);
        }

        private void Update() {
            if (Agent.radius >= Size) {
                Destroy(this.gameObject);
            }
        }
    }
}
