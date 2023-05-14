using System;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _SYSTEMS_._InventorySystem_.ScriptableO
{
	public enum InventoryOwner
	{
		Player,
		Workshop,
		AI,
		Other
	}
	
	[Serializable]
	[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory", order = 0)]
	public class Inventory : SerializedScriptableObject
	{
		[ShowInInspector] [OnValueChanged("OnInventorySizeChanged")]
		public Vector2 inventorySize;
		public int maxItemCount = 64;
		[FormerlySerializedAs("inventoryName")] public InventoryOwner inventoryOwner;

		[TableMatrix(HorizontalTitle = "Inventory Array",
			SquareCells = true,DrawElementMethod = "DrawElement"),ShowInInspector] 
		public Item[,] InventoryArray;
		
		private static Item DrawElement(Rect rect, Item item)
		{
			if (item == null) return null;
			var text = item == null ? "Empty" : item.itemName;

			if (item != null)
				if (item is IStackable)
					text += " X" + item.currentStackCount;

			var image = item?.icon.texture;

			var content = new GUIContent(text, image);
			if (item == null)
			{
				GUI.Label(rect, content);
				return null;
			}

			GUI.Label(rect, content);
			return item;
		}

		private void OnInventorySizeChanged()
		{
			InventoryArray = new Item[(int)inventorySize.x, (int)inventorySize.y];
		}

		public void CreateInventory(Vector2 size,InventoryOwner owner,int maxItemCount = 64)
		{
			inventoryOwner = owner;
			inventorySize = size;
			this.maxItemCount = maxItemCount;
			InventoryArray = new Item[(int)inventorySize.x, (int)inventorySize.y];
		}
		
	}
}