using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using UnityEngine;
namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items
{
	[CreateAssetMenu(fileName = "New UsableAndSellableItem", menuName = "Inventory System/Items/UsableAndSellableItem")]
	public class UsableAndSellableItem :UsableItem, ISellable
	{
		public override void Use()
		{
			base.Use();
			if (currentStackCount == 0)
			{
				Debug.Log("No more item");
				return;
			}
			Debug.Log("Used");
		}

		public void Sell(ref Inventory inventory)
		{
			//inventory.AddGold(price);
			inventory.SellItem(this);
		}
	}
}

