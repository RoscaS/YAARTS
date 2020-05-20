using System;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

namespace Components
{
    public abstract class BaseActionsComponent : MonoBehaviour
    {
        [HideInInspector] public bool isBUilding;
        protected Entity entity;
        public List<IGUIAction> actions = new List<IGUIAction>();

    }
}
