using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;

namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension
{
	public static class InventoryRemove
	{
		public static void RemoveItem(this Inventory inventory, Item item)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] == null)
						continue;

					if (inventory.InventoryArray[x, y].id != item.id)
						continue;

					inventory.InventoryArray[x, y] = null;
					if(inventory.inventoryOwner == InventoryOwner.Player)
						InventorySystem.OnRemovedItem?.Invoke(item);
					return;
				}
			}
		}

		public static void ClearInventory(this Inventory inventory)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					inventory.InventoryArray[x, y] = null;
				}
			}
		}
	}
}