using System.IO;
using System.Linq;
using UnityEditor;

namespace MaiBase
{
    public static class EnumCreator
    {
#if UNITY_EDITOR
        private static readonly string _basePath = "Assets/MaiGames/Resources/EnumStorage/";

        public static void CreateEnum(this string[] itemsToEnum,string itemName)
        {
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }

            var item = itemName + ".cs";
            var allPath = _basePath + item;

            var fileInside = "public enum Mai_" + itemName + "{ None,";
            if (itemsToEnum.Length > 0)
            {
                foreach (var enumItem in itemsToEnum)
                {
                    var newItem = enumItem.Replace(" ", "_");

                    fileInside += " " + newItem;
                    if (newItem != itemsToEnum.Last())
                        fileInside += ",";
                }

                fileInside += "}";
            }
            else fileInside += "}";

            File.WriteAllText(allPath, fileInside);
            AssetDatabase.Refresh();
        }

#endif
    }
}