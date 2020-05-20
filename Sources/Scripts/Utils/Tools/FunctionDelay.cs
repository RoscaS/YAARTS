using System;
using System.Collections.Generic;
using System.Linq;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;

namespace Utils
{
    /**
    * Triggers an Action after a delay
    */
    public class FunctionDelay
    {
        public const string INITIALIZATION_GAME_OBJECT = "DelaysInstances";

        private static readonly Transform InitializationGameObject =
                GameObject.Find(INITIALIZATION_GAME_OBJECT).transform;


        /**
         * Compagnon object to hook into Monobehaviour life cycle.
         */
        public class MonoBehaviorHook : MonoBehaviour
        {
            public Action onUpdate;

            public void SetAction(Action action) => onUpdate = action;

            private void Start() {
                transform.parent = InitializationGameObject;
            }

            private void Update() {
                onUpdate.Invoke();
            }
        }


        /*------------------------------------------------------------------*\
        |*							Attributes
        \*------------------------------------------------------------------*/

        private static readonly List<FunctionDelay> timers = new List<FunctionDelay>();

        protected float counter;
        protected readonly string tag;
        protected readonly Action action;
        protected readonly GameObject gameObject;

        /*------------------------------------------------------------------*\
        |*							Constructor
        \*------------------------------------------------------------------*/

        protected FunctionDelay(Action action, float delay, string tag, GameObject gameObject) {
            this.gameObject = gameObject;
            this.action = action;
            this.counter = delay;
            this.tag = tag;
        }

        /*------------------------------------------------------------------*\
        |*							Instance level
        \*------------------------------------------------------------------*/

        protected void Update() {
            counter -= Time.deltaTime;

            if (counter <= 0) {
                action.Invoke();
                Destroy();
            }
        }

        private void Destroy() {
            UnityEngine.Object.Destroy(gameObject);
            RemoveTimer(this);
        }

        /*------------------------------------------------------------------*\
        |*							Class level
        \*------------------------------------------------------------------*/

        private static void RemoveTimer(FunctionDelay functionDelay) {
            timers.Remove(functionDelay);
        }

        public static void Create(Action action, float timer, string tag = null) {
            var components = new[] { typeof(MonoBehaviorHook) };
            var gameObject = new GameObject("FunctionDelay", components);

            var functionTimer = new FunctionDelay(action, timer, tag, gameObject);
            gameObject.GetComponent<MonoBehaviorHook>().SetAction(functionTimer.Update);

            timers.Add(functionTimer);
        }

        public static void Stop(string tag) {
            timers.Where(t => t.tag == tag).ForEach(t => t.Destroy());
        }
    }
}
