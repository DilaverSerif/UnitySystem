using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using UnityEngine;

public class PlayerInventory : Bag
{
    public Inventory leftHandInventory;
    public Inventory rightHandInventory;

    protected override void Awake()
    {
        base.Awake();
        CurrentInventory.AllNull();
        // theInventory = ScriptableObject.CreateInstance<Inventory>();
        // theInventory.CreateInventory(Vector2.one * 5,InventoryOwner.Player);
        leftHandInventory = ScriptableObject.CreateInstance<Inventory>();
        leftHandInventory.CreateInventory(new Vector2(1,0),InventoryOwner.Player,1);
        rightHandInventory = ScriptableObject.CreateInstance<Inventory>();
        rightHandInventory.CreateInventory(new Vector2(1,0),InventoryOwner.Player,1);
    }
}
