using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_.Extension;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_.ScriptableO
{
    [CreateAssetMenu(fileName = "ItemLoader", menuName = "Inventory System/ItemLoader", order = 1)]
    public class ItemLoader : SingletonScriptableObject<ItemLoader>
    {
        public Item[] items;
        
        public Item GetItem(string itemName)
        {
            foreach (var item in items)
            {
                if (item.itemName == itemName)
                    return Object.Instantiate(item);
            }

            return null;
        }
        
        public Item GetItem(int id)
        {
            foreach (var item in items)
            {
                if (item.id == id)
                    return item;
            }

            return null;
        }
        
        public Item GetItem(Item item)
        {
            foreach (var item1 in items)
            {
                if (item1 == item)
                    return item1;
            }

            return null;
        }
    }
}
