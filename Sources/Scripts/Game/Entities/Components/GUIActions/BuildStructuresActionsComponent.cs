using System.Linq;
using Game.Factories;

namespace Components
{
    public class BuildStructuresActionsComponent : BaseActionsComponent
    {

        /*------------------------------------------------------------------*\
        |*							HOOKS
        \*------------------------------------------------------------------*/

        private void Start() {
            entity = GetComponent<Entity>();
            actions = EntityFactory.Instance.buildingsModels
                                   .Select(i => i.Meta.Build(builder: entity))
                                   .ToList();
        }
    }
}
