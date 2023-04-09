using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_._InventorySystem_.Extension;
using Sirenix.OdinInspector;
using UnityEngine;
namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items
{
	[CreateAssetMenu(fileName = "New Stackable Item", menuName = "Inventory System/Items/Sellable Item")]
	public class SellableItem : Item, ISellable
	{
		public void Sell(ref Inventory inventory)
		{
			inventory.AddGold(price);
			inventory.SellItem(this);
		}

	}
}
