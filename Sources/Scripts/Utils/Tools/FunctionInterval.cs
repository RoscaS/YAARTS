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
    public class FunctionInterval
    {

        private static readonly Transform InitializationGameObject =
                GameObject.Find(FunctionDelay.INITIALIZATION_GAME_OBJECT).transform;



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

        private static readonly List<FunctionInterval> timers = new List<FunctionInterval>();

        private float counter;
        private readonly string tag;
        private readonly Action action;
        private readonly GameObject gameObject;

        private readonly float interval;
        private int ticks;

        /*------------------------------------------------------------------*\
        |*							Constructor
        \*------------------------------------------------------------------*/

        private FunctionInterval(Action action, float interval, string tag, GameObject go, int ticks) {
            this.gameObject = go;
            this.action = action;
            this.interval = interval;
            this.tag = tag;
            this.ticks = ticks == 0 ? -1 : ticks;
        }

        /*------------------------------------------------------------------*\
        |*							Instance level
        \*------------------------------------------------------------------*/

        public void Update() {
            counter -= Time.deltaTime;

            if (counter <= 0) {
                action.Invoke();
                counter = interval;

                if (--ticks == 0) {
                    Destroy();
                }
            }
        }

        private void Destroy() {
            UnityEngine.Object.Destroy(gameObject);
            RemoveTimer(this);
        }

        /*------------------------------------------------------------------*\
        |*							Class level
        \*------------------------------------------------------------------*/

        private static void RemoveTimer(FunctionInterval functionTimer) {
            timers.Remove(functionTimer);
        }

        public static void Create(Action action, float timer, int ticks = 0, string tag = null) {
            var components = new[] { typeof(MonoBehaviorHook) };
            var gameObject = new GameObject("FunctionInterval", components);

            var functionTimer = new FunctionInterval(action, timer, tag, gameObject, ticks);
            gameObject.GetComponent<MonoBehaviorHook>().SetAction(functionTimer.Update);

            timers.Add(functionTimer);
        }

        public static void Stop(string tag) {
            var list = timers.Where(t => t.tag == tag).ToList();
            list.ForEach(i => i.Destroy());
        }
    }
}
