using _GAME_.Scripts._SYSTEMS_._InventorySystem_.ScriptableO;
using UnityEngine;

namespace _SYSTEMS_._InventorySystem_.Abstract
{
    public abstract class Bag : MonoBehaviour
    {
        public Inventory[] Inventories;
        public Inventory theBag=> Inventories[0];

        protected virtual void Awake()
        {
            for (var index = 0; index < Inventories.Length; index++)
            {
                Inventories[index] = Instantiate(Inventories[index]);
            }
        }
    }
}