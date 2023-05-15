
namespace _SYSTEMS_._Interaction_System_.Abstract
{
    public interface ICanInteract<T>
    {
        public bool CanInteract();
        public void InteractionUpdate();
    }
}