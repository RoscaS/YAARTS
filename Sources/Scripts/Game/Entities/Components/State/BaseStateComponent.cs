using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components.State
{
    public abstract class BaseStateComponent : MonoBehaviour
    {
        protected Entity entity;
        public IState Current { get; set; }
        public string currentState;

        [HideInInspector] public Dictionary<Type, IState> states;

        protected virtual void Start() {
            entity = GetComponent<Entity>();
            InitStates();
        }

        public virtual void Update() {
            if (!entity.disabled) Current?.Update();
        }

        public void SetState(IState state) {
            Current?.OnExit();
            Current = state;
            currentState = state.ToString();
            Current.OnEntry();
        }

        protected abstract void InitStates();
    }
}
