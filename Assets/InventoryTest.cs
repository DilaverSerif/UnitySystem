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
}