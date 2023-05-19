using _SYSTEMS_._InventorySystem_.ScriptableO;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_.Abstract
{
    public abstract class Bag : MonoBehaviour
    {
        public Inventory[] inventories;

        public Inventory CurrentInventory
        {
            get
            {
                if (inventories.Length == 0) return null;
                return inventories[0];
            }
        }

        protected virtual void Awake()
        {
            // for (var index = 0; index < inventories.Length; index++)
            // {
            //     inventories[index] = Instantiate(inventories[index]);
            // }
        }   
    }
}