using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using UnityEngine;
namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items
{
	[CreateAssetMenu(fileName = "New Stackable Item", menuName = "Inventory System/Items/Stackable Item")]
	public class StackableItem : Item, IStackable
	{
		public void AddCount(int count)
		{
			currentStackCount += count;
			Debug.Log("Added");
		}
	}
}