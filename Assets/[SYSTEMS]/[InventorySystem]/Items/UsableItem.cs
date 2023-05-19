using _SYSTEMS_._Interaction_System_.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _SYSTEMS_._InventorySystem_.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Create An UsableItem")]
    public class UsableItem : Item, IUsable<GameObject>
    {
        [BoxGroup("Item Data/Usable")]
        public UnityEvent onUse;
        [BoxGroup("Item Data/Usable")]
        public UnityEvent onStopUse;
        
        public void Use(GameObject target)
        {
            onUse?.Invoke();
        }

        public void StopUse(GameObject target)
        {
            onStopUse?.Invoke();
        }
    }
}
