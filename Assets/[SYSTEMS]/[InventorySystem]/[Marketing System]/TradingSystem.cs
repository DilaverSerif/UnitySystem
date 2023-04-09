using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_._InventorySystem_.Extension;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_._Marketing_System_
{
    public static class TradingSystem
    {
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