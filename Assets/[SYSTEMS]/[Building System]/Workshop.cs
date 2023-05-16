using System.Collections;
using _SYSTEMS_._Building_System_.Abstract;
using _SYSTEMS_._Building_System_.ScriptableO;
using _SYSTEMS_._Character_Controller_.States;
using _SYSTEMS_._InventorySystem_.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _SYSTEMS_._Building_System_
{
    public enum TypeOfReturn
    {
        ReturnToGround,
        ReturnToBag
    }

    public abstract class Workshop : UpgradableBuilding
    {
        [Title("Workshop Data")] 
        public BuildingData buildingData;
        
        protected Coroutine ProductionCoroutine;
        protected WaitForSeconds WaitForProductionDelay;

        public TypeOfReturn typeOfReturn;
        [ShowInInspector, ReadOnly, BoxGroup("Upgrade UI")]
        protected int ProductionCount;

        [Title("SpawnArea Settings")] public float radius = 1f;
        public Vector3 offset = Vector3.zero;
        
        [Title("Events")]
        public UnityEvent onProductionThrow;
        public UnityEvent onProductionProduced;
        
        private float _throwTime;
        
        protected override void SetCountText()
        {
            if (upgradeData == null) return;
            if (upgradeData.IsFinish())
                productionCountText.text = $"<sprite name={buildingData.producingItem.itemName}> \n" + ProductionCount;
                
            else base.SetCountText();
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out Bag _)) 
                return;
            SendItems();
        }
        
        protected override void UpgradeFinished(int level)
        {
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

        protected bool IsFullProduction()
        {
            return ProductionCount == buildingData.buildingUpgradeData[upgradeData.upgradeCurrentLevel].maxProduction;
        }

        protected abstract void ThrowToGround();
        protected abstract void ThrowToBag();

        protected virtual IEnumerator ProductionPlace()
        {
            while (!IsFullProduction())
            {
                yield return WaitForProductionDelay;
                onProductionProduced?.Invoke();
                ProductionCount++;
                SetCountText();
            }

            ProductionCoroutine = null;
        }
        
        private void SendItems()
        {
            _throwTime += Time.deltaTime;
            if (_throwTime < buildingData.throwDelay) 
                return;
            
            ProductionCount--;
            
            switch (typeOfReturn)
            {
                case TypeOfReturn.ReturnToBag:
                    ThrowToBag();
                    break;
                case TypeOfReturn.ReturnToGround:
                    ThrowToGround();
                    break;
                default:
                    Debug.LogWarning("Building System: TypeOfReturn is not selected!");
                    break;
            }

            SetCountText();
            onProductionThrow?.Invoke();
            _throwTime = 0;
            
            if (ProductionCoroutine == null & !IsFullProduction())
                ProductionCoroutine = StartCoroutine(ProductionPlace());
        }
        
        protected void OnDrawGizmosSelected()
        {
            #if UNITY_EDITOR
            ExtensionMethods.DrawDisc(transform.position + offset, radius, Color.blue);
            #endif
        }
    }
}