using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_.Extension;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_.Extension
{
    public static class InventoryActions
    {
        public static bool SlotIsEmpty(this Inventory inventory, InventorySlot slot)
        {
            return inventory.InventoryArray[slot.x, slot.y] == null;
        }

        public static bool SellItem(this Inventory inventory, Item item, int amount = 1)
        {
            var foundItem = inventory.GetItem(item);

            if (foundItem == null) //Envanterda item yok
            {
                "Item not found".Log(SystemsEnum.InventorySystem);
                return false;
            }

            foundItem.SellItem(amount);
            
            return true;
        }


        public static bool UseItem(this Inventory inventory, Item item, int amount = 1)
        {
            var foundItem = inventory.GetItem(item);

            if (foundItem == null) //Envanterda item yok
            {
                "Item not found".Log(SystemsEnum.InventorySystem);
                return false;
            }
            
            foundItem.UsedItem(amount);
            
            return true;
        }
        
        public static bool
            GiveItem(this Inventory inventory, Item item, Vector3 startPoint, Vector3 endPoint) //Birinden almak için
        {
            if (inventory.UseItem(item))
            {
                // var spawnedObject = item.prefab.Spawn(startPoint, Quaternion.identity).transform;
                //
                // spawnedObject.DOJump(endPoint, 1, 1, 1).OnComplete(
                //     () => { spawnedObject.gameObject.Despawn(); }
                // );

                return true;
            }

            return false;
        }

        public static bool TakeItem(this Inventory inventory, Inventory targetInv, Item item,
            Vector3 startPoint,
            Vector3 endPoint) //Birine vermek için
        {
            if (!inventory.UseItem(item))
            {
                Debug.LogWarning($"Inventory System: " + $"Item not found - {item.itemName} - TakeItem");
                return false;
            }

            // var spawnedObject = item.prefab.Spawn(startPoint, Quaternion.identity).transform;
            //
            // spawnedObject.DOJump(endPoint, 1, 1, 1).OnComplete(
            //     () =>
            //     {
            //         spawnedObject.gameObject.Despawn();
            //         targetInv.AutoAddNewItem(item);
            //     }
            // );

            return true;
        }
    }
}