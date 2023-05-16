using _SYSTEMS_._Building_System_.Abstract;
using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _SYSTEMS_._Building_System_
{
    public abstract class Warehouse : UpgradableBuilding
    {
        [FormerlySerializedAs("Inventory")] [FormerlySerializedAs("shelfInventory")] [Title("Shelf Data")]
        public Inventory inventory;
        public int warehouseCapacity;
        public Vector2 warehouseSize;
        protected override void Awake()
        {
            inventory = ScriptableObject.CreateInstance<Inventory>();
            inventory.CreateInventory(warehouseSize,InventoryOwner.Workshop,warehouseCapacity);
            base.Awake();
        }
        
        protected virtual void PutItemToInventory(Item item)
        {
            if (inventory.GetItem(item).Item)
                return;
            
            if (inventory.IsFull())
                return;
            
            inventory.AutoAddNewItem(item);
        }
        
        protected virtual void GetItemToInventory(Item item, int count)
        {
            if (!inventory.GetItem(item).Item)
                return;
            
            inventory.AutoAddNewItem(item);
        }
        
    }
}
