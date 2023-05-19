using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_.Extension;


namespace _SYSTEMS_._InventorySystem_.Extension
{
    public static class InventoryAdd
    {
        public static Item AddItem(this Inventory inventory, Item item, InventorySlot slot, int amount = 1)
        {
            if (!inventory.SlotIsEmpty(slot))
                return null;

            inventory.InventoryArray[slot.x, slot.y] = new InventoryItem(item);
            ("Item added: " + item.name).Log(SystemsEnum.InventorySystem);
            return item;
        }


        public static bool AutoAddNewItem(this Inventory inventory, Item item)
        {
            if (inventory.IsFull()) return false;

            var inventoryItem = inventory.GetItem(item);


            if (inventoryItem != null)
            {
                if (item == null) return false;
                if (!item.IsStackable) return false;
                inventoryItem.count += item.amount;
                
                if (inventoryItem.count > item.maxStackCount)
                    inventoryItem.count = item.maxStackCount;
                return true;
            }


            inventory.AddItem(item, inventory.GetEmptySlot().Slot);
            return true;
        }

        public static void RemoveItem(this Inventory inventory, Item item)
        {
            for (var x = 0; x < inventory.inventorySize.x; x++)
            {
                for (var y = 0; y < inventory.inventorySize.y; y++)
                {
                    if (inventory.InventoryArray[x, y] == null)
                        continue;

                    if (inventory.InventoryArray[x, y].Item.id != item.id)
                        continue;

                    inventory.InventoryArray[x, y] = null;
                    return;
                }
            }
        }

        public static void ClearInventory(this Inventory inventory)
        {
            for (var x = 0; x < inventory.inventorySize.x; x++)
            {
                for (var y = 0; y < inventory.inventorySize.y; y++)
                {
                    inventory.InventoryArray[x, y] = null;
                }
            }
        }
    }
}