using System.Collections;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.ScriptableO;
using _SYSTEMS_.Extension;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Building_System_
{
    public abstract class WorkShopWithRequirement : Workshop
    {
        [BoxGroup("Raw WorkShop")]
        public Item inputItem;

        private Inventory _inputInventory;
        private Coroutine _inputAtCoroutine;
        private WaitForSeconds _waitForInputDelay;
        [BoxGroup("Raw WorkShop")]
        public int inventorySize;
        public float inputDelay = 0.5f;

        private bool _continueInput;

        [ShowInInspector, ReadOnly,BoxGroup("Raw WorkShop")]
        public int InputCount
        {
            get
            {
                if (_inputInventory == null)
                    return 0;
                
                if(_inputInventory.GetItemBySlot(new InventorySlot(0, 0)) == null)
                    return 0;

                return _inputInventory.GetItemBySlot(new InventorySlot(0, 0)).count;
            }
        }
        
        public bool EmptyInventory() => ProductionCount == 0 && InputCount == 0;


        protected override void Awake()
        {
            base.Awake();
            _inputInventory = ScriptableObject.CreateInstance<Inventory>();
            _inputInventory.CreateInventory(Vector2.one,InventoryOwner.Workshop,inventorySize);
            _waitForInputDelay = new WaitForSeconds(inputDelay);
        }

        protected override void OnWorkShopEnter()
        {
            base.OnWorkShopEnter();
            _continueInput = true;
            if (CurrentBag.CurrentInventory.GetItem(inputItem).Item)
                if (_inputAtCoroutine != null)
                    StopCoroutine(_inputAtCoroutine);
            
            _inputAtCoroutine = StartCoroutine(InputItems(CurrentBag));
        }

        protected override void OnWorkShopExit()
        {
            base.OnWorkShopExit();
            _continueInput = false;
        }

        //Uretim icin gerekli itemleri al
        private IEnumerator InputItems(Bag bag) 
        {
            if(_inputInventory.IsFull()) 
                yield break;
            
            while (bag.CurrentInventory.GetItem(inputItem).Item & _continueInput) //Gerekli itemler var mı?
            {
                yield return _waitForInputDelay;
                bag.CurrentInventory.TakeItem(_inputInventory, inputItem, bag.transform.position, transform.position);
                SetCountText();
                if(_inputInventory.IsFull()) 
                    break;
            }
            
            _inputAtCoroutine = null;
            _continueInput = false;
            "Building System: Not Found Item".Log(SystemsEnum.CraftingSystem);
        }

        protected override IEnumerator ProductionPlace()
        {
            while (!IsFullProduction())
            {
                yield return WaitForProductionDelay;
                _inputInventory.UseItem(inputItem);
                ProductionCount++;
                SetCountText();

                while (InputCount == 0)
                    yield return null;
            }

            ProductionCoroutine = null;
        }
        
    }
}