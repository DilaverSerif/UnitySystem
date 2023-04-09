using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using UnityEngine;
namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items
{
	[CreateAssetMenu(fileName = "New UsableAndStackableItem", menuName = "Inventory System/Items/UsableAndStackableItem")]
	public sealed class UsableAndStackableItem :UsableItem, IStackable
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

		public void AddCount(int count)
		{
			currentStackCount += count;
			Debug.Log("Added");
		}
	}
}