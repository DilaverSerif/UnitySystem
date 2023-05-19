namespace _SYSTEMS_._Interaction_System_.Abstract
{
    public interface IUsable<T>
    {
        void Use(T target);
        void StopUse(T target);
    }
}