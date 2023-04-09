using System.Collections.Generic;
using _SYSTEMS_._Upgrade_System_.Class;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _SYSTEMS_._Upgrade_System_.ScriptableO
{
	[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade System/Upgrade", order = 0)]
	public class Upgrade : ScriptableObject
	{
		public int upgradeCurrentLevel;

		public List<RequirementLevelArray> requirementsForUpgrade;
		public UnityEvent<int> upgradeEffect;
		[ShowInInspector]
		protected virtual int MaxLevel => requirementsForUpgrade.Count;

		public bool IsEmpty()
		{
			return requirementsForUpgrade.Count == 0;
		}

		public bool IsFinish()
		{
			return upgradeCurrentLevel >= MaxLevel;
		}

		public RequirementLevelArray GetCurrentRequirementsForUpgrade()
		{
			return GetRequirementsForUpgrade(upgradeCurrentLevel);
		}

		public RequirementLevelArray GetRequirementsForUpgrade(int level)
		{
			if (level < requirementsForUpgrade.Count)
				return requirementsForUpgrade[level];
			return null;
		}
	}

	public enum UpgradeState
	{
		FinishLevel,
		FinishUpgrade,
		AddedItem,
		NotFound,
		NotNecessary,
		Necessary,
		WrongItem,
		AlreadyFinish
	}
}