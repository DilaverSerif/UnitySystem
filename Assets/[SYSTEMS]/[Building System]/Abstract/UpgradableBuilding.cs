using System.Collections;
using _SYSTEMS_._InventorySystem_._Marketing_System_;
using _SYSTEMS_._InventorySystem_.Abstract;
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
    public class UpgradableBuilding : MonoBehaviour
    {
        [Title("Upgrade Trigger Data")] public Upgrade upgradeData;

        public float dropDelay = 0.15f;

        public Transform[] upgradeParts;
        private Upgrade _targetUpgrade;

        private Coroutine _upgradeCoroutine;
        protected WaitForSeconds WaitForUpgradeDelay;

        [ReadOnly, BoxGroup("Upgrade UI")] public TextMeshProUGUI productionCountText;
        
        public UnityEvent OnUpgradeFinished;
        
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
        }

        protected virtual void OnDisable()
        {
            if(upgradeData == null) return;
            upgradeData.upgradeEffect.RemoveListener(UpgradeFinished);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bag component))
                OnWorkShopEnter(ref component);
        }
        
        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Bag component))
                OnWorkShopExit(ref component);
        }

        protected virtual void SetCountText()
        {
            if (upgradeData.IsFinish()) return;

            var prodText = "";

            foreach (var selectedReq in upgradeData.GetCurrentRequirementsForUpgrade().requirementsForUpgrade)
            {
                prodText += $"<sprite name={selectedReq.reqItem.name}> | {selectedReq.CurrentRequiredAmount}\n";
            }

            productionCountText.text = prodText;
        }

        protected virtual bool GetItem(ref Bag playerBag) //Upgrade icin gerekli itemler var mı?
        {
            var items = upgradeData.GetNecessaryItems();
            if (items == null) return false;

            foreach (var item in items)
            {
                if (!playerBag.theBag.GiveItem(item, playerBag.transform.position, transform.position))
                    continue;

                upgradeData.AddCount(item);
                //TODO: Toplu item atma acip kapama
                return true;
            }
            
            return true;
        }

        protected virtual IEnumerator Upgrade(Bag component)
        {
            while (GetItem(ref component)) //Gerekli itemler var mı?
            {
                yield return WaitForUpgradeDelay;
                SetCountText();
            }

            if (upgradeData.IsFinish())
                productionCountText.DOFade(0, 0.25f);
            _upgradeCoroutine = null;
        }

        protected virtual void UpgradeFinished(int level)
        {
            ("Upgrade Finished: " + level).Log(SystemsEnum.MarketingSystem);
            if (upgradeParts.Length <= level) return;
            
            foreach (var part in upgradeParts)
                part.gameObject.SetActive(false);
            
            
            OnUpgradeFinished?.Invoke();
            upgradeParts[level].gameObject.SetActive(true);
        }
        
        protected virtual void OnWorkShopEnter(ref Bag component)
        {
            if (upgradeData.IsFinish()) return;
            if (_upgradeCoroutine != null) return;
            _upgradeCoroutine = StartCoroutine(Upgrade(component));
        }

        protected virtual void OnWorkShopExit(ref Bag component)
        {
            if (_upgradeCoroutine != null)
                StopCoroutine(_upgradeCoroutine);
            _upgradeCoroutine = null;
        }
    }
}