using System;
using _SYSTEMS_._InventorySystem_.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _SYSTEMS_._InventorySystem_.ScriptableO
{
    public enum InventoryOwner
    {
        Player,
        Workshop,
        AI,
        Other
    }

    [Serializable]
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory", order = 0)]
    public class Inventory : SerializedScriptableObject
    {
        [ShowInInspector] [OnValueChanged("OnInventorySizeChanged")]
        public Vector2 inventorySize;

        public int maxItemCount = 64;

        [FormerlySerializedAs("inventoryName")]
        public InventoryOwner inventoryOwner;

        [TableMatrix(HorizontalTitle = "Inventory Array",
             SquareCells = true, DrawElementMethod = "DrawElement"), ShowInInspector]
        public InventoryItem[,] InventoryArray;

        private static InventoryItem DrawElement(Rect rect, InventoryItem inventoryItem)
        {
            if (inventoryItem == null) return null;
            var text = inventoryItem.Item == null ? "Empty" : inventoryItem.Item.itemName;

            if (inventoryItem.Item != null)
                if (inventoryItem.Item.IsStackable)
                    text += " X" + inventoryItem.count;

            var image = inventoryItem.Item?.icon.texture;

            var content = new GUIContent(text, image);
            if (inventoryItem.Item == null)
            {
                GUI.Label(rect, content);
                return null;
            }

            GUI.Label(rect, content);
            return inventoryItem;
        }
// #if UNITY_EDITOR
//         private static InventoryItem DrawPieceMeshes(Rect rect, InventoryItem piece)
//         {
//             return Sirenix.Utilities.Editor.SirenixEditorFields.PreviewObjectField(rect, piece);
//         }
// #endif
        
        public void AllNull()
        {
            for (var i = 0; i < inventorySize.x; i++)
            for (var j = 0; j < inventorySize.y; j++)
                InventoryArray[i, j] = null;
        }

        private void OnInventorySizeChanged()
        {
            InventoryArray = new InventoryItem[(int)inventorySize.x, (int)inventorySize.y];
        }

        public void CreateInventory(Vector2 size, InventoryOwner owner, int maxItemCount = 64)
        {
            inventoryOwner = owner;
            inventorySize = size;
            this.maxItemCount = maxItemCount;
            InventoryArray = new InventoryItem[(int)inventorySize.x, (int)inventorySize.y];
        }
    }
}