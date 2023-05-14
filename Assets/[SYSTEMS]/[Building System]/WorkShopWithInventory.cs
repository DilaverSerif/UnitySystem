using System.Collections;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_._Marketing_System_;
using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Building_System_.Abstract
{
    public class WorkShopWithInventory : Workshop
    {
        [BoxGroup("Raw WorkShop")]
        public Item inputItem;
        protected Inventory _inputInventory;
        private Coroutine _inputAtCoroutine;
        [BoxGroup("Raw WorkShop")]
        public int inventorySize;

        [ShowInInspector, ReadOnly,BoxGroup("Raw WorkShop")]
        public int InputCount
        {
            get
            {
                if (_inputInventory == null)
                    return 0;
                if(_inputInventory.GetItemBySlot(new InventorySlot(0, 0)) == null)
                    return 0;

                return _inputInventory.GetItemBySlot(new InventorySlot(0, 0)).currentStackCount;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _inputInventory = ScriptableObject.CreateInstance<Inventory>();
            _inputInventory.CreateInventory(Vector2.one,InventoryOwner.Workshop);
        }

        protected override void OnWorkShopEnter(ref Bag component)
        {
            base.OnWorkShopEnter(ref component);
            if (component.theBag.GetItem(inputItem) & _inputAtCoroutine == null)
                _inputAtCoroutine = StartCoroutine(InputItems(component));
        }

        protected override void OnWorkShopExit(ref Bag component)
        {
            base.OnWorkShopExit(ref component);

            if (_inputAtCoroutine != null)
            {
                StopCoroutine(_inputAtCoroutine);
                _inputAtCoroutine = null;
            }
        }

        private IEnumerator InputItems(Bag bag) //Yapiyi insa etmek icin gerekli itemler var mi yok mu kontrol eder
        {
            if (!upgradeData.IsFinish()) yield break;
            if(_inputInventory.IsFull()) yield break;
            
            while (bag.theBag.GetItem(inputItem)) //Gerekli itemler var mı?
            {
                yield return WaitForProductionThrowDelay;
                bag.theBag.TakeItem(_inputInventory, inputItem, bag.transform.position, transform.position);
                SetCountText();
                if(_inputInventory.IsFull()) break;
            }

            Debug.Log("Building Systen: Not Found Item");
        }

        protected override IEnumerator ProductionPlace()
        {
            Animator.SetBool(Working, true);

            while (!IsFullProduction())
            {
                yield return WaitForProductionDelay;
                _inputInventory.UseItem(inputItem);
                ProductionCount++;
                SetCountText();

                while (InputCount == 0)
                {
                    yield return null;
                }
            }

            Animator.SetBool(Working, false);
            ProductionCoroutine = null;
        }
        
        public bool EmptyInventory()
        {
            return ProductionCount == 0 && InputCount == 0;
        }
    }
}