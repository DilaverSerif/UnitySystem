using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _SYSTEMS_._Save_System_
{
    public static class DataManager<T> where T : SaveableData<T>
    {
        public static readonly List<T> SaveableDataList = new List<T>();

        public static void SaveData()
        {
            var saveableData = Resources.LoadAll<T>($"Saves");

            for (var index = 0; index < saveableData.Length; index++)
            {
                SaveableDataList[index].Data = saveableData[index].Data;
            }
        }

        public static void LoadData(string fileName)
        {
            LoadDataFromResources();
            Debug.Log(SaveableDataList[0].GetObject<int>("Test"));
        }

        private static void LoadDataFromResources()
        {
            var saveableDataList = new List<T>();

            var loadAll = Resources.LoadAll<T>($"Saves");

            for (var index = 0; index < loadAll.Length; index++)
            {
                saveableDataList.Add(Object.Instantiate(loadAll[index]));
            }
        }
    }
}