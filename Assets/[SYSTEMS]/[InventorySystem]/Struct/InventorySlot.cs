using System;

namespace _GAME_.Scripts._SYSTEMS_._InventorySystem_.Extension
{
	[Serializable]
	public struct InventorySlot
	{
		public int x;
		public int y;

		public InventorySlot(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}