using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items
{
	public class Item : ScriptableObject
	{
		[BoxGroup("Item Data")]
		[BoxGroup("Item Data/General")]
		public int id;
		[BoxGroup("Item Data/General")]
		public string itemName;
		[BoxGroup("Item Data/Sellable")] 
		public int price;
		[BoxGroup("Item Data/Stackable"),ReadOnly]
		public int currentStackCount;
		[BoxGroup("Item Data/Stackable")] 
		public int maxStackCount;
		
		[PreviewField(35, ObjectFieldAlignment.Center)]
		[BoxGroup("Item Data/Visual")]
		public Sprite icon;
		
		[PreviewField(35, ObjectFieldAlignment.Center)]
		[BoxGroup("Item Data/Visual")]
		public GameObject prefab;
	}
}