using UnityEngine;

namespace _SYSTEMS_._Interaction_System_.Abstract
{
    [CreateAssetMenu(menuName = "Interaction Base/Create GameObject Collectable", fileName = "GameObject Collectable", order = 0)]
    public class MonoBehaviourCollectable : InteractionBase<MonoBehaviour>, ICollectableBase
    {
        public override void TouchInteract(MonoBehaviour touchObject)
        {
            throw new System.NotImplementedException();
        }

        public override void ExitInteract(MonoBehaviour touchObject)
        {
            throw new System.NotImplementedException();
        }

        public void Effect()
        {
            
        }
    }
}