using System;
using _SYSTEMS_._InventorySystem_.Items;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_
{
    public static class InventorySystem
    {
        public static Action<Item,int> OnUsedAnItem;
        public static Action<Item,int> OnSoldItem;
        
        public static Action<Item> OnAddedNewItem;
        public static Action<Item> OnRemovedItem;
        
        public static Action<Item> OnLootItem;
        public static Action<GameObject> OnLootGameObject;
        
        public static Action<Item> OnSpawnItem;
        public static Action<GameObject> OnSpawnGameObject;
    }
}
