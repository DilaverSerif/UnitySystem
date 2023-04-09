using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_._InventorySystem_.Extension;
using UnityEngine;
namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items
{
	[CreateAssetMenu(fileName = "New Stackable Item", menuName = "Inventory System/Items/Sellable And StackableItem")]
	public class SellableAndStackableItem : Item, ISellable,IStackable
	{
		public void Sell(ref Inventory inventory)
		{
			inventory.AddGold(price);
			inventory.SellItem(this);
		}

		public void AddCount(int count)
		{
			if(maxStackCount < currentStackCount + count)
				currentStackCount = maxStackCount;
			else
			{
				currentStackCount += count;
			}
		}
	}
}