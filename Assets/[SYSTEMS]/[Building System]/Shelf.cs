using _SYSTEMS_._Building_System_.Abstract;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Building_System_
{
    public class Shelf : UpgradableBuilding
    {
        [Title("Shelf Data")]
        public Inventory shelfInventory;
        public int shelfCapacity;
        public int shelfLevel;
        protected override void Awake()
        {
            shelfInventory = ScriptableObject.CreateInstance<Inventory>();
            shelfInventory.CreateInventory(Vector2.one * shelfLevel,InventoryOwner.Workshop,shelfCapacity);
            base.Awake();
        }
    }
}
