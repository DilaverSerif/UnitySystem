using _GAME_.Scripts._SYSTEMS_._InventorySystem_;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_._Marketing_System_;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_.Extension;

namespace _SYSTEMS_._InventorySystem_.Extension
{
    public static class InventoryActions
    {
        public static bool SlotIsEmpty(this Inventory inventory, InventorySlot slot)
        {
            if (inventory.InventoryArray[slot.x, slot.y] == null)
                return true;
            return false;
        }

        public static bool SellItem(this Inventory inventory, Item item)
        {
            var foundItem = inventory.GetItem(item);

            if (foundItem == null) //Envanterda item yok
            {
                "Item not found".Log(SystemsEnum.InventorySystem);
                return false;
            }

            if (foundItem is not ISellable sellableItem)
            {
                "This item is not sellable".Log(SystemsEnum.InventorySystem);
                return false;
            }


            if (foundItem is IStackable stackableItem && foundItem.currentStackCount <= 0)
            {
                inventory.RemoveItem(foundItem);
                "Remove Item".Log(SystemsEnum.InventorySystem);
                InventorySystem.OnRemovedItem?.Invoke(foundItem);
            }
            else
            {
                foundItem.currentStackCount--;
                "Remove Item".Log(SystemsEnum.InventorySystem);
                InventorySystem.OnRemovedItem?.Invoke(foundItem);
            }

            MarketSystem.OnSellAnItem?.Invoke(foundItem, inventory);
            return true;
        }


        public static bool UseItem(this Inventory inventory, Item item)
        {
            var foundItem = inventory.GetItem(item);

            if (foundItem == null) //Envanterda item yok
            {
                "Item not found".Log(SystemsEnum.InventorySystem);
                return false;
            }

            if (foundItem is IUsable usableItem) //Check if item is usable
            {
                usableItem.Use();
                InventorySystem.OnUsedAnItem?.Invoke(usableItem as Item);
            }

            if (foundItem is IStackable iStack) //Check if item is stackable
            {
                iStack.AddCount(-1); //Remove 1 from stack

                if (foundItem.currentStackCount <= 0) //If stack is empty
                {
                    inventory.RemoveItem(foundItem);
                    "Remove Item".Log(SystemsEnum.InventorySystem);
                    InventorySystem.OnRemovedItem?.Invoke(iStack as Item);
                    return true;
                }
                
                InventorySystem.OnRemovedItem?.Invoke(iStack as Item);
                return true;
            }

            inventory.RemoveItem(foundItem);
            "Remove Item".Log(SystemsEnum.InventorySystem);
            InventorySystem.OnRemovedItem?.Invoke(foundItem);
            return true;
        }
    }
}