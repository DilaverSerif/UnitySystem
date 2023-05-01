using UnityEngine;

namespace _SYSTEMS_._Interaction_System_.Abstract
{
    public interface IInteractionBase<T>
    {
        void TouchInteract(T touchObject);
        void ExitInteract(T touchObject);
    }

    public abstract class InteractionBase<T> : ScriptableObject, IInteractionBase<T>
    {
        public abstract void TouchInteract(T touchObject);
        public abstract void ExitInteract(T touchObject);

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out T touchObject))
                TouchInteract(touchObject);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out T touchObject))
                ExitInteract(touchObject);
        }
    }
}