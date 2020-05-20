using Game.Controllers;
using Game.Factories;
using Game.Interfaces;
using Utils;

namespace Components.Actions
{
    public class BuildAction : IGUIAction
    {
        public Entity Built { get; set; }
        public Entity Builder { get; set; }
        private bool cooldown;

        public BuildAction(Entity built, Entity builder) {
            Built = built;
            Builder = builder;
        }

        public void Callback() {
            if (cooldown) return;
            cooldown = true;
            FunctionDelay.Create(() => cooldown = false, .3f);

            if (PlayerController.Instance.CheckPrice(Built)) {
                if (Built.Meta.IsStructure) {
                    BuildingSpawner.Instance.SetCurrent(Built, Builder);
                }
                else {
                    CharacterSpawner.Instance.StartTraining(Built, Builder);
                }
            }
        }

    }
}
