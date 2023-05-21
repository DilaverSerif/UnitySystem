using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Interface;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_.Items
{
    public class CollectableItem : MonoBehaviour, ICollectable
    {
        public Item dataItem;

        public void Collect(Inventory inventory)
        {
            inventory.AutoAddNewItem(dataItem);
            gameObject.SetActive(false);
        }
    }
    
}