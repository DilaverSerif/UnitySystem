using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_.Items
{
	[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Create An Item")]
	public class Item : ScriptableObject
	{
		[BoxGroup("Item Data")]
		[BoxGroup("Item Data/General")]
		public int id;
		[BoxGroup("Item Data/General")]
		public string itemName;
		[BoxGroup("Item Data/Sellable")] 
		public int price;
		[BoxGroup("Item Data/Stackable")] 
		public int maxStackCount;
		[BoxGroup("Item Data/Stackable")] 
		public int amount = 1;
		[PreviewField(35, ObjectFieldAlignment.Center)]
		[BoxGroup("Item Data/Visual")]
		public Sprite icon;
		
		[PreviewField(35, ObjectFieldAlignment.Center)]
		[BoxGroup("Item Data/Visual")]
		public GameObject prefab;
		
		[ShowInInspector, ReadOnly, BoxGroup("Item Data/Stackable")]
		public bool IsStackable => maxStackCount > 1;
		[ShowInInspector, ReadOnly, BoxGroup("Item Data/Sellable")]
		public bool IsSellable => price > 0;
	}
}