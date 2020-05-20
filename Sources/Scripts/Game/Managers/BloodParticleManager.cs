using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BloodParticleManager : MonoBehaviour
{
    public static BloodParticleManager Instance { get; private set; }

    public List<Single> singleList;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
        singleList = new List<Single>();
    }

    private void Update() {
        for (int i = 0; i < singleList.Count; i++) {
            Single single = singleList[i];
            single.Update();

            if (single.IsParticleComplete()) {
                singleList.RemoveAt(i);
                i--;
            }
        }
    }

    /*------------------------------------------------------------------*\
    |*							PUBLIC METHODES
    \*------------------------------------------------------------------*/

    public void SpawnBlood(Vector3 position, Vector3 direction, float distanceMax, float particles) {
        for (int i = 0; i < Random.Range(4, particles); i++) {
            var newDirection = Quaternion.AngleAxis(Random.Range(-35, 35), Vector3.up) * direction;
            newDirection.y = 0;
            var distance = Random.Range(0, distanceMax);
            var single = new Single(position, newDirection, distance);
            singleList.Add(single);
        }
    }

    /*------------------------------------------------------------------*\
    |*							COMPAGNION
    \*------------------------------------------------------------------*/

    public class Single
    {
        private static readonly MeshParticleController meshParticleController = MeshParticleController.Instance;

        private static readonly Vector3 size = new Vector3(.2f, .2f);

        private Vector3 position;
        private readonly Vector3 direction;
        private float speed;
        private float rotation;

        public readonly int quadIdx;
        private readonly int uvIdx;

        public Single(Vector3 position, Vector3 direction, float distanceMax) {
            this.position = position;
            this.direction = direction;
            this.rotation = Random.Range(0, 360f);
            this.speed = Random.Range(1f, distanceMax + 1);
            this.uvIdx = Random.Range(0, 8);

            quadIdx = meshParticleController.AddQuad(position, rotation, size, false, uvIdx);
        }


        public void Update() {
            position += direction * (speed * Time.deltaTime);
            rotation += 360f * (speed / 10f) * Time.deltaTime;

            meshParticleController.UpdateQuad(quadIdx, position, rotation, size, false, uvIdx);

            float slowDownFactor = 3.5f;
            speed -= speed * slowDownFactor * Time.deltaTime;
        }

        public bool IsParticleComplete() {
            return speed < .1f;
        }
    }

    /*------------------------------------------------------------------*\
    |*							TESTS METHODES
    \*------------------------------------------------------------------*/

    private static void BloodTests() {
        var distance = 5f;
        var position = Helpers.MouseWorldPosition();
        var direction = Helpers.Random2DDirection();
        var single = new Single(position, direction, distance);
        Instance.singleList.Add(single);
    }

    private static void BloodPattern() {
        for (int i = -4; i < 4; i++) {
            for (int j = 0; j < 16; j++) {
                var position = new Vector3(j, 0.01f, i);
                var direction = Vector3.zero;
                var single = new Single(position, direction, i + 4);
                Instance.singleList.Add(single);
            }
        }
    }
}
