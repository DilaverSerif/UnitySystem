using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_.Extension;

namespace _SYSTEMS_._InventorySystem_.Extension
{
	public enum ItemState
	{
		NotEnough,
		ThereIs,
		None
	}
	public static class InventoryGets
	{
		public static SlotData GetEmptySlot(this Inventory inventory)
		{
			var slotData = new SlotData();
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] != null) continue;

					slotData.Empty = true;
					slotData.Slot = new InventorySlot(x, y);
					return slotData;
				}
			}

			return default(SlotData);
		}
		
		public static InventoryItem GetInventoryItemBySlot(this Inventory inventory,
			Item item)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] == null)
						continue;

					if (inventory.InventoryArray[x, y].Item.id == item.id)
					{
						return inventory.InventoryArray[x, y];
					}
				}
			}

			return null;
		}
		
		public static ItemState CheckItem(this Inventory inventory, Item itemData, int count = 1)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] == null)
						continue;

					if (inventory.InventoryArray[x, y].Item.id == itemData.id)
					{
						return inventory.InventoryArray[x, y].count >= count
							? ItemState.ThereIs
							: ItemState.NotEnough;
					}
				}
			}

			return ItemState.None;
		}

		public static InventoryItem GetItem(this Inventory inventory, Item itemData)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] == null)
						continue;

					if (inventory.InventoryArray[x, y].Item.id == itemData.id)
					{
						return inventory.InventoryArray[x, y];
					}
				}
			}

			"Item not found".Log(SystemsEnum.InventorySystem);
			return null;
		}
		
		public static InventoryItem GetItemById(this Inventory inventory, int index)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] == null)
						continue;

					if (inventory.InventoryArray[x, y].Item.id == index)
					{
						return inventory.InventoryArray[x, y];
					}
				}
			}

			"Item not found".Log(SystemsEnum.InventorySystem);
			return null;
		}
		
		public static int GetCurrentItemCount(this Inventory inventory, InventoryItem itemData)
		{
			var getItem = inventory.GetItem(itemData.Item);
			
			if (getItem == null)
			{
				"Item not found".Log(SystemsEnum.InventorySystem);
				return 0;
			}

			return itemData.count;
		}	
		
		public static int GetCurrentItemCount(this Inventory inventory, int index)
		{
			var getItem = inventory.GetItemById(index);
			return getItem == null ? 0 : inventory.GetCurrentItemCount(getItem);
		}	

		public static InventoryItem GetItemBySlot(this Inventory inventory, InventorySlot slot)
		{
			if (inventory == null)
			{
				"Inventory Sytem: Null Inventory".Log(SystemsEnum.InventorySystem);
				return null;
			}
			var check = inventory.InventoryArray[slot.x, slot.y];
			if (check != null) return inventory.InventoryArray[slot.x, slot.y];

			"Item not found".Log(SystemsEnum.InventorySystem);
			return null;
		}

		public static int GetCurrentItemCountAllInventory(this Inventory inventory)
		{
			var totalCurrentCount = 0;

			foreach (var inventoryItem in inventory.InventoryArray)
			{
				if (inventoryItem == null)
					continue;

				if (inventoryItem.Item.IsStackable)
					totalCurrentCount += inventoryItem.count;
				else totalCurrentCount++;
			}

			return totalCurrentCount;
		}

		public static bool IsFull(this Inventory inventory)
		{
			return inventory.GetCurrentItemCountAllInventory() >= inventory.maxItemCount;
		}

		public struct SlotData
		{
			public bool Empty;
			public InventorySlot Slot;
		}
	}
}