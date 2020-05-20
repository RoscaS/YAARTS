using System.Collections.Generic;
using System.Linq;
using Game.Enums;
using Game.Factories;
using UnityEngine;

namespace Components
{
    public class BuildCharacterActionsComponent : BaseActionsComponent
    {
        public EntityClass type;

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            entity = GetComponent<Entity>();

            List<Entity> models = new List<Entity>();

            if (type == EntityClass.Archery) models = EntityFactory.Instance.rangedModels;
            else if (type == EntityClass.Barracks) models = EntityFactory.Instance.meleeModels;
            else if (type == EntityClass.TownCenter) models = EntityFactory.Instance.workersModels;

            actions = models.Select(i => i.Meta.Build(builder: entity)).ToList();
        }
    }
}
