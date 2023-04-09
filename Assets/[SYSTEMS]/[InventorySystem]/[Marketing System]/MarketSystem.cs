using System;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.ScriptableO;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_._Marketing_System_
{
    public static class MarketSystem
    {
        public static Action<Item, Inventory> OnSellAnItem;
        public static Action<Item, Inventory> OnBuyItem;

        public static Action<Item, Inventory, Inventory> OnBuyItemWithTargetInventory;

        public static void SellQuickItem(this Item item, Inventory inventory)
        {
            if (item is not ISellable sellable)
            {
                Debug.Log("Item is not sellable");
                return;
            }

            sellable.Sell(ref inventory);
        }
    }
}