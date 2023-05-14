using System.Collections;
using _GAME_.Scripts._SYSTEMS_._Building_System_.ScriptableO;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._Character_Controller_.States;
using _SYSTEMS_._InventorySystem_.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _SYSTEMS_._Building_System_.Abstract
{
    public enum TypeOfReturn
    {
        ReturnToGround,
        ReturnToBag
    }

    public class Workshop : UpgradableBuilding
    {
        [Title("Workshop Data")] public BuildingData buildingData;
        
        protected Coroutine SendItemsCoroutine;
        protected Coroutine ProductionCoroutine;

        protected WaitForSeconds WaitForProductionDelay;
        protected WaitForSeconds WaitForProductionThrowDelay;

        public TypeOfReturn typeOfReturn;
        [ShowInInspector, ReadOnly, BoxGroup("Upgrade UI")]
        protected int ProductionCount;
        public int CurrentProductionCount => ProductionCount;

        [Title("SpawnArea Settings")] public float radius = 1f;
        public Vector3 offset = Vector3.zero;

        protected Animator Animator;
        protected readonly int Working = Animator.StringToHash("Working");
        [HideInInspector]
        public UnityEvent OnProductionThrow;
        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponentInChildren<Animator>();
            if(upgradeData == null)
                return;
            WaitForProductionThrowDelay = new WaitForSeconds(buildingData.throwDelay);
        }

        // protected override void Start()
        // {
        //     base.Start();
        //     if (upgradeData.IsFinish())
        //         ProductionCoroutine = StartCoroutine(ProductionPlace());
        // }

        protected override void SetCountText()
        {
            if (upgradeData == null) return;
            if (upgradeData.IsFinish())
                productionCountText.text = $"<sprite name={buildingData.producingItem.itemName}> \n" + ProductionCount;
                
            else base.SetCountText();
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out Bag component)) return;
            Debug.Log("OnTriggerStay");
            if (SendItemsCoroutine == null & ProductionCount > 0)
                SendItemsCoroutine = StartCoroutine(SendItems(component));
        }
        
        protected override void UpgradeFinished(int level)
        {
            Debug.Log(transform.name + " Opened Level:" + level);
            base.UpgradeFinished(level);
            if (level == 0) return;

            if (ProductionCoroutine != null)
            {
                StopCoroutine(ProductionCoroutine);
                ProductionCoroutine = null;
            }

            WaitForProductionDelay =
                new WaitForSeconds(buildingData.buildingUpgradeData[upgradeData.upgradeCurrentLevel].productionDelay);
            
            if(ProductionCoroutine == null)
                ProductionCoroutine = StartCoroutine(ProductionPlace());
        }

        protected override IEnumerator Upgrade(Bag component)
        {
            while (GetItem(ref component)) //Gerekli itemler var mı?
            {
                yield return WaitForUpgradeDelay;
                SetCountText();
            }
        }

        protected virtual bool IsFullProduction()
        {
            return ProductionCount == buildingData.buildingUpgradeData[upgradeData.upgradeCurrentLevel].maxProduction;
        }
        
        protected virtual void ThrowToGround()
        {
            var position = transform.position;
            //TODO: POOL ENTEGRE
            //var spawnedItem = buildingData.producingItem.prefab.Spawn(position, Quaternion.identity).transform;
            //spawnedItem.DOJump(position.GetRandomPointInRadius(ref radius, ref offset), 1, 1, 1);
            ProductionCount--;
        }

        protected virtual void ThrowToBag(ref Bag bag)
        {
            if (bag.theBag.IsFull())
            {
                Debug.Log("Building System: Bag is full!");
                return;
            }
            var position = transform.position;
            // var spawnedItem = buildingData.producingItem.prefab.Spawn(position, Quaternion.identity).transform;
            // spawnedItem.GetComponent<CollectableBase<Bag>>().Collect(bag,.75f);
            ProductionCount--;
            OnProductionThrow?.Invoke();
        }

        protected virtual IEnumerator ProductionPlace()
        {
            Animator.SetBool(Working, true);

            while (!IsFullProduction())
            {
                yield return WaitForProductionDelay;
                ProductionCount++;
                SetCountText();
            }

            Animator.SetBool(Working, false);
            ProductionCoroutine = null;
        }

        private IEnumerator SendItems(Bag bag)
        {
            yield return WaitForProductionThrowDelay;

            switch (typeOfReturn)
            {
                case TypeOfReturn.ReturnToBag:
                    ThrowToBag(ref bag);
                    break;
                case TypeOfReturn.ReturnToGround:
                    ThrowToGround();
                    break;
                default:
                    Debug.LogWarning("Building System: TypeOfReturn is not selected!");
                    break;
            }

            SetCountText();
            if (ProductionCoroutine == null)
                ProductionCoroutine = StartCoroutine(ProductionPlace());
            SendItemsCoroutine = null;
        }
        
        protected void OnDrawGizmosSelected()
        {
            #if UNITY_EDITOR
            ExtensionMethods.DrawDisc(transform.position + offset, radius, Color.blue);
#endif
        }

        // public void Save()
        // {
        //     ES3.Save("upgradableBuildings"+ gameObject.name, upgradeData);
        // }
        //
        // public void Load()
        // {
        //     if(!ES3.KeyExists("upgradableBuildings"+ gameObject.name))return;
        //     upgradeData =  ((Upgrade)ES3.Load("upgradableBuildings"+ gameObject.name));
        // }
    }
}