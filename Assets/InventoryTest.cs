using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using Sirenix.OdinInspector;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory targetInventory;

    [Button]
    public void AddItem(Item item)
    {
        targetInventory.AutoAddNewItem(item);
    }
    
    [Button]
    public void RemoveItem(Item item)
    {
        targetInventory.RemoveItem(item);
    }
    
    [Button]
    public void InventoryToText()
    {
        foreach (var inventoryItem in targetInventory.InventoryArray)
        {
            if (inventoryItem == null)
            {
                Debug.Log("Empty");
                continue;
            }
            Debug.Log(inventoryItem.Item.name + " x" + inventoryItem.count);
        }
    }
    
}