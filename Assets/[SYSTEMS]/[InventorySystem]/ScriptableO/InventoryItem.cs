using System;
using _SYSTEMS_._InventorySystem_.Items;

namespace _SYSTEMS_._InventorySystem_.ScriptableO
{
    [Serializable]
    public class InventoryItem
    {
        private Item _item;
        public Item Item => _item;
        public int count;

        public InventoryItem(Item item)
        {
            _item = item;
            count = item.amount;
        }
        
    }
}