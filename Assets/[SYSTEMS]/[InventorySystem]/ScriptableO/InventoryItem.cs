using System;
using _SYSTEMS_._Interaction_System_.Abstract;
using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using Object = UnityEngine.Object;

namespace _SYSTEMS_._InventorySystem_.ScriptableO
{
    [Serializable]
    public class InventoryItem
    {
        private Item _item;
        public Item Item => _item;
        public int count;
        private Inventory _inventory;

        public InventoryItem(Item item, Inventory inventory)
        {
            _item = item;
            count = item.amount;
            _inventory = inventory;
        }

        public void SetItem(Item comingItem)
        {
            if (comingItem == null) return;

            if (count + comingItem.amount > comingItem.maxStackCount) return;
            if (count >= comingItem.maxStackCount) return;


            _item = Object.Instantiate(comingItem);
            count = Item.amount;

            if (_inventory.inventoryOwner == InventoryOwner.Player)
                InventorySystem.OnAddedNewItem?.Invoke(_item);

            if (count > 0) return;

            if (_inventory.inventoryOwner == InventoryOwner.Player)
            {
                InventorySystem.OnRemovedItem?.Invoke(Item);
                _inventory.RemoveItem(_item);
            }
        }

        public void AddItem(int amount = 1)
        {
            if (Item == null) return;
            if (!Item.IsStackable) return;
            count += amount;
            if (count > Item.maxStackCount)
                count = Item.maxStackCount;
        }

        public void UsedItem(int amount = 1)
        {
            if (Item == null) return;
            if (Item is IUsable usableItem)
                usableItem.Use();
            else return;

            count -= amount;
            if (count > 0) return;

            if (_inventory.inventoryOwner == InventoryOwner.Player)
            {
                InventorySystem.OnRemovedItem?.Invoke(Item);
                _inventory.RemoveItem(_item);
            }
        }

        public void SellItem(int amount = 1)
        {
            if (Item == null) return;
            if (!Item.IsSellable)
                return;

            var price = Item.price * amount;
            count -= amount;
            //Add coin
            if (_inventory.inventoryOwner == InventoryOwner.Player)
                InventorySystem.OnSoldItem?.Invoke(Item, amount);
            if (count > 0) return;
            _item = null;
            count = 0;
        }
    }
}