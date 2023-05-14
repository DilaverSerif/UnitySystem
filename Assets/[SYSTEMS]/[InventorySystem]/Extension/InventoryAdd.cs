using _GAME_.Scripts._SYSTEMS_._InventorySystem_;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_.Extension;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_.Extension
{
    public static class InventoryAdd
    {
        public static Item AddItem(this Inventory inventory, Item item, InventorySlot slot, int amount = 1)
        {
            if (!inventory.SlotIsEmpty(slot))
                return null;

            inventory.InventoryArray[slot.x, slot.y] = item;

            if (item is IStackable stackableItem)
                item.currentStackCount += amount;

            if (inventory.inventoryOwner == InventoryOwner.Player)
                InventorySystem.OnAddedNewItem?.Invoke(item);

            return item;
        }


        public static bool AutoAddNewItem(this Inventory inventory, Item item, int amount = 1)
        {
            if (inventory.IsFull()) return false;

            if (inventory.GetItem(item) is IStackable stackable)
            {
                item = stackable as Item;
                stackable.AddCount(amount);
                $"{item.itemName} count: {item.currentStackCount}".Log(SystemsEnum.InventorySystem);

                if (inventory.inventoryOwner == InventoryOwner.Player)
                    InventorySystem.OnAddedNewItem?.Invoke(item);

                return true;
            }

            //Item yoksa
            if (!inventory.GetEmptySlot().Empty)
                return false;

            var clonedItem = Object.Instantiate(item);
            inventory.AddItem(clonedItem, inventory.GetEmptySlot().Slot, amount);
            return true;
        }
    }
}