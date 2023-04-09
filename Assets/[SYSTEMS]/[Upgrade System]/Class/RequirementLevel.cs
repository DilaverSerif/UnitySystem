using System;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Upgrade_System_.Class
{
    public enum RequirementStat
    {
        Finish,
        NotAdded,
        AddedItem,
        NotFinish
    }


    [Serializable]
    public class RequirementLevel
    {
        public Item reqItem;
        public int requiredAmount;
        [ReadOnly] public int currentAmount;
        [ReadOnly] public RequirementStat reqStat = RequirementStat.NotFinish;

        public int CurrentRequiredAmount => requiredAmount - currentAmount; //Kalan item sayısını verir

        public RequirementStat AddItemRequirement(ref Item item, int count = 1)
        {
            if (reqItem != item | currentAmount >= requiredAmount)
            {
                Debug.Log("Full or Wrong Item");
                return RequirementStat.NotAdded;
            }

            currentAmount += count;

            if (currentAmount >= requiredAmount)
            {
                Debug.Log("Finish Requirement");
                reqStat = RequirementStat.Finish;
                return RequirementStat.Finish;
            }

            return RequirementStat.AddedItem;
        }
    }
}