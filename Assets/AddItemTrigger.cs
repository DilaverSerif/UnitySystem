using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using Sirenix.OdinInspector;

public class AddItemTrigger : Trigger<Bag>
{
    [Title("Item")] 
    public Item item;
    
    protected override void SuccessfulTrigger()
    {
        base.SuccessfulTrigger();
        if (TheGeneric == null)
            return;
        TheGeneric.CurrentInventory.AutoAddNewItem(item);
    }

    public override void StopUse()
    {
        
    }
}