using System.Collections;
using _SYSTEMS_._Character_Controller_;
using _SYSTEMS_._Interaction_System_.Abstract;
using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._InventorySystem_.Extension;
using _SYSTEMS_._Upgrade_System_.Extension;
using _SYSTEMS_._Upgrade_System_.ScriptableO;
using _SYSTEMS_.Extension;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _SYSTEMS_._Building_System_.Abstract
{
    public abstract class UpgradableBuilding : MonoBehaviour, IUsable<PlayerController>
    {
        [Title("Upgrade Trigger Data")] public Upgrade upgradeData;

        public float dropDelay = 0.15f;
        public bool bulkDrop;
        private Upgrade _targetUpgrade;

        private Coroutine _upgradeCoroutine;
        protected WaitForSeconds WaitForUpgradeDelay;

        [ReadOnly, BoxGroup("Upgrade UI")] 
        public TextMeshProUGUI productionCountText;
        
        public UnityEvent<int> OnUpgradeFinished;
        protected bool ContinueUpgrade;
        protected Bag CurrentBag;
        protected virtual void Awake()
        {
            if(upgradeData == null)
                return;
            upgradeData = Instantiate(upgradeData);
            productionCountText = GetComponentInChildren<TextMeshProUGUI>(true);
           
            WaitForUpgradeDelay = new WaitForSeconds(dropDelay);
        }

        protected virtual void Start()
        {
            if(upgradeData == null) return;
            UpgradeFinished(upgradeData.upgradeCurrentLevel);
        }

        protected virtual void OnEnable()
        {
            SetCountText();
            if(upgradeData == null) return;
            if (upgradeData.IsFinish()) return;
            upgradeData.upgradeEffect.AddListener(UpgradeFinished);
            OnUpgradeFinished.AddListener(LevelUp);
        }

        protected virtual void OnDisable()
        {
            if(upgradeData == null) return;
            upgradeData.upgradeEffect.RemoveListener(UpgradeFinished);
            OnUpgradeFinished.RemoveListener(LevelUp);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bag component))
                CurrentBag = component;
        }
        
        protected virtual void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Bag component)) return;
            
            ContinueUpgrade = false;
            CurrentBag = null;
        }
        
        /**
         * Gerekli olan itemlerin adlarini ve sayilarini UI'da gosterir.
         */

        protected virtual void SetCountText() 
        {
            if(upgradeData == null) return;
            if (upgradeData.IsFinish()) return;

            var prodText = "";

            foreach (var selectedReq in upgradeData.GetCurrentRequirementsForUpgrade().requirementsForUpgrade)
            {
                prodText += $"<sprite name={selectedReq.reqItem.name}> | {selectedReq.CurrentRequiredAmount}\n";
            }

            productionCountText.text = prodText;
        }
        
        protected virtual IEnumerator Upgrade(Bag component)
        {
            while (GetItem(ref component) & ContinueUpgrade) //Gerekli itemler var mı? Check eder
            {
                yield return WaitForUpgradeDelay;
                SetCountText();
            }

            _upgradeCoroutine = null;
            ContinueUpgrade = false;

            if (!upgradeData.IsFinish()) yield break;
            productionCountText.DOFade(0, 0.25f);
        }
        
        /**
         * Upgrade icin gerekli itemler var mı?
         */
        protected virtual bool GetItem(ref Bag playerBag)
        {
            var items = upgradeData.GetNecessaryItems();
            if (items == null) return false;

            foreach (var item in items)
            {
                if (!playerBag.CurrentInventory.GiveItem(item, playerBag.transform.position, transform.position))
                    continue;

                upgradeData.AddCount(item);
               
                if(bulkDrop) //Toplu item atma acip kapama
                    return true;
            }
            
            return true;
        }
        
        protected virtual void UpgradeFinished(int level)
        {
            ("Upgrade Finished: " + level).Log(SystemsEnum.CraftingSystem);
            OnUpgradeFinished?.Invoke(level);
        }
        
        protected abstract void LevelUp(int level);
        
        protected virtual void OnWorkShopEnter()
        {
            if (upgradeData.IsFinish()) return;
            if (_upgradeCoroutine != null)
            {
                StopCoroutine(_upgradeCoroutine);
                _upgradeCoroutine = null;
            }
            
            _upgradeCoroutine = StartCoroutine(Upgrade(CurrentBag));
        }

        protected virtual void OnWorkShopExit()
        {
            _upgradeCoroutine = null;
            ContinueUpgrade = false;
        }
        
        public void Use(PlayerController target)
        {
            ContinueUpgrade = true;
            OnWorkShopEnter();
        }

        public void StopUse(PlayerController target)
        {
            OnWorkShopExit();
        }
    }
}