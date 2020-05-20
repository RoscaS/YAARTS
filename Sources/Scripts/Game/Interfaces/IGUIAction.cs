
namespace Game.Interfaces
{
    public interface IGUIAction
    {
        Entity Built { get; set; }
        Entity Builder { get; set; }

        void Callback();
    }
}
