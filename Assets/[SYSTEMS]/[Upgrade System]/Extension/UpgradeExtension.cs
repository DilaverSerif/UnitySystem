using System.Collections.Generic;
using System.Linq;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._Upgrade_System_.Class;
using _SYSTEMS_._Upgrade_System_.ScriptableO;
using UnityEngine;

namespace _SYSTEMS_._Upgrade_System_.Extension
{
    public static class UpgradeExtension
    {
        public static void AddCount(this Upgrade upgradeData, Item itemName, int count = 1)
        {
            if (upgradeData.IsEmpty())
            {
                Debug.LogWarning("Upgrade is empty");
                return;
            }

            switch (upgradeData.AddItemToUpgrade(itemName, count))
            {
                case UpgradeState.FinishLevel:
                    Debug.Log("Level complete - upgrade");
                    return;
                case UpgradeState.AddedItem:
                    Debug.Log(itemName.itemName + " Item added");
                    break;
                case UpgradeState.NotFound:
                    Debug.Log(itemName.itemName + " Item not found");
                    break;
                case UpgradeState.FinishUpgrade:
                    Debug.Log(upgradeData.upgradeCurrentLevel + " Upgrade complete");
                    break;
                case UpgradeState.WrongItem:
                    Debug.LogWarning(itemName.itemName + " Wrong item");
                    break;
                case UpgradeState.NotNecessary:
                    Debug.LogWarning(itemName.itemName + " Item not necessary");
                    break;
                default:
                    Debug.LogError("Upgrade state not found");
                    break;
            }
        }

        private static UpgradeState AddItemToUpgrade(this Upgrade upgrade, Item itemName, int amount = 1)
        {
            if (upgrade.IsFinish()) return UpgradeState.AlreadyFinish;

            var currentRequirements = upgrade.GetCurrentRequirementsForUpgrade();

            if (currentRequirements == null)
                return UpgradeState.NotFound;

            foreach (var upgradeItem in currentRequirements.GetRequirements())
            {
                switch (upgradeItem.AddItemRequirement(ref itemName, amount))
                {
                    case RequirementStat.AddedItem:
                        return UpgradeState.AddedItem;

                    case RequirementStat.NotAdded:
                        if (currentRequirements.requirementsForUpgrade.Last() == upgradeItem)
                            return UpgradeState.WrongItem;
                        continue;

                    case RequirementStat.Finish:
                        if (AllRequirementsFinish(upgrade))
                        {
                            Debug.LogWarning("Finish Upgrade");
                            return UpgradeState.FinishUpgrade;
                        }

                        Debug.LogWarning("Finish Level");
                        return UpgradeState.FinishLevel;

                    case RequirementStat.NotFinish:
                    default:
                        Debug.LogError("Requirement state not found");
                        break;
                }
            }

            Debug.Log("Not Found");
            return UpgradeState.NotFound;
        }

        public static bool AllRequirementsFinish(this Upgrade upgrade) //Upgrade Bitti mi
        {
            var currentRequirements = upgrade.GetCurrentRequirementsForUpgrade();

            if (currentRequirements == null)
            {
                Debug.LogError("Current Requirements is null");
                return false;
            }

            foreach (var upgradeItem in currentRequirements.requirementsForUpgrade)
            {
                if (upgradeItem.reqStat != RequirementStat.Finish)
                {
                    return false;
                }
            }

            upgrade.upgradeCurrentLevel++;
            upgrade.upgradeEffect.Invoke(upgrade.upgradeCurrentLevel);

            return upgrade.IsFinish();
        }

        public static Item[] GetNecessaryItems(this Upgrade upgrade)
        {
            var currentReq = upgrade.GetCurrentRequirementsForUpgrade();
            if (currentReq == null) return null;

            var items = new List<Item>();
            for (var i = 0; i < currentReq.requirementsForUpgrade.Length; i++)
            {
                if (currentReq.requirementsForUpgrade[i].reqStat == RequirementStat.Finish)
                    continue;

                items.Add(currentReq.requirementsForUpgrade[i].reqItem);
            }

            return items.ToArray();
        }
    }
}