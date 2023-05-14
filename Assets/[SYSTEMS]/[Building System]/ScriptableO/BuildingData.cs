using System;
using _GAME_.Scripts._SYSTEMS_._InventorySystem_.Items;
using _SYSTEMS_._InventorySystem_.Abstract;
using UnityEngine;

namespace _GAME_.Scripts._SYSTEMS_._Building_System_.ScriptableO
{
	[CreateAssetMenu(menuName = "Building System/Create BuildingData", fileName = "BuildingData", order = 0)]
	public class BuildingData : ScriptableObject
	{
		public Item producingItem;
		public float throwDelay = 0.15f;
		public BuildingUpgradeData[] buildingUpgradeData;

		[Serializable]
		public struct BuildingUpgradeData
		{
			public float productionDelay;
			public int maxProduction;
		}
	}
}