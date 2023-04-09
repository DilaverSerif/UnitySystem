using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.ScriptableO;
using UnityEngine;

namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension
{
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

		public static Item GetItem(this Inventory inventory, Item itemData)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] == null)
						continue;

					if (inventory.InventoryArray[x, y].id == itemData.id)
					{
						return inventory.InventoryArray[x, y];
					}
				}
			}

			Debug.Log("Item not found");
			return null;
		}
		
		public static Item GetItemById(this Inventory inventory, int index)
		{
			for (var x = 0; x < inventory.inventorySize.x; x++)
			{
				for (var y = 0; y < inventory.inventorySize.y; y++)
				{
					if (inventory.InventoryArray[x, y] == null)
						continue;

					if (inventory.InventoryArray[x, y].id == index)
					{
						return inventory.InventoryArray[x, y];
					}
				}
			}

			Debug.Log("Item not found");
			return null;
		}
		
		public static int GetCurrentCount(this Inventory inventory, Item itemData)
		{
			var getItem = inventory.GetItem(itemData);
			
			if (getItem == null)
			{
				Debug.Log("Item not found");
				return 0;
			}

			return getItem.currentStackCount;
		}	
		
		public static int GetCurrentCountByIndex(this Inventory inventory, int index)
		{
			var getItem = inventory.GetItemById(index);
			return getItem == null ? 0 : inventory.GetCurrentCount(getItem);
		}	

		public static Item GetItemBySlot(this Inventory inventory, InventorySlot slot)
		{
			if (inventory == null)
			{
				Debug.LogError("Inventory Sytem: Null Inventory");
				return null;
			}
			var check = inventory.InventoryArray[slot.x, slot.y];
			if (check != null) return inventory.InventoryArray[slot.x, slot.y];

			Debug.Log("Slot is empty");
			return null;
		}

		public static int GetCurrentItemCount(this Inventory inventory)
		{
			var totalCurrentCount = 0;

			foreach (var item in inventory.InventoryArray)
			{
				if (item == null)
					continue;

				if (item is IStackable)
					totalCurrentCount += item.currentStackCount;
				else totalCurrentCount++;
			}

			return totalCurrentCount;
		}

		public static bool IsFull(this Inventory inventory)
		{
			return inventory.GetCurrentItemCount() >= inventory.maxItemCount;
		}

		public struct SlotData
		{
			public bool Empty;
			public InventorySlot Slot;
		}
	}
}