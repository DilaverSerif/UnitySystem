using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items
{
	[CreateAssetMenu(fileName = "New Stackable Item", menuName = "Inventory System/Items/Usable Item")]
	public class UsableItem : Item, IUsable
	{
		public UnityEvent OnUse;
		public virtual void Use()
		{
			OnUse.Invoke();
			Debug.Log("Used");
		}
	}
}