using System.Collections.Generic;
using _SYSTEMS_.Extension;
using MaiBase;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Menu_System_
{
    public class MenuManager : SerializedSingleton<MenuManager>
    {
        public MenuContainer[] Containers;
        [ShowInInspector]
        public Dictionary<string, GameObject> MenuItems = new Dictionary<string, GameObject>();

        [Button]
        private void PullContainers()
        {
            MenuItems.Clear();
            Containers = GetComponentsInChildren<MenuContainer>();

            foreach (var container in Containers)
            {
                container.menuSubItems = container.GetComponentsInChildren<MenuSubItem>();
            }
            
            var stringContainers = new string[Containers.Length];
            for (var i = 0; i < Containers.Length; i++)
                stringContainers[i] = Containers[i].transform.name;
            
            stringContainers.CreateEnum("MenuContainers");
            
            
            for (var i = 0; i < stringContainers.Length; i++)
            {
                var stringItems = new string[Containers[i].menuSubItems.Length]; //Her bir container icin itemlarin isimleri

                for (var j = 0; j < Containers[i].menuSubItems.Length; j++)
                {
                    stringItems[j] = Containers[i].menuSubItems[j].transform.name; //Item isimini al ve arraye ekle
                    var itemEnum = "Mai_" + stringContainers[i] + stringItems[j];

                    MenuItems.Add(itemEnum, Containers[i].menuSubItems[j].gameObject);
                }
                
                stringItems.CreateEnum(stringContainers[i]);
            }
        }
        
        
        internal T GetItemByEnum <T> (string itemKey) where T : Component
        {
            if(itemKey == null)
                return null;
            
            var item = MenuItems.TryGetValue(itemKey,out var value) ? value : null;
            if (item == null)
            {
                Debug.LogError("Not Found Menu Item");
                return null;
            }
            
            if(item.TryGetComponent(out T component))
                return component;

            return null;
        }
        
        internal T GetMenuByEnum <T> (string itemKey) where T : Component
        {
            if(itemKey == null)
                return null;
            
            T item = null;

            foreach (var container in Containers)
            {
                if(container.transform.name == itemKey)
                    if (container.TryGetComponent(out T component))
                        return component;
                    else
                    {
                        Debug.LogError("Not found Menu");
                        return null;
                    }
            }

            return null;
        }
    }
}