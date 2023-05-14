using System;
using UnityEngine;

namespace _SYSTEMS_._Menu_System_
{
    public static class MenuExtension
    {
        public static T GetMenuItem<T>(this Enum itemEnum) where T : Component
        {
            return MenuManager.Instance.GetItemByEnum<T>(itemEnum.GetType().Name+itemEnum);
        }
        
        public static T GetMenu<T>(this Mai_MenuContainers itemEnum) where T : Component
        {
            return MenuManager.Instance.GetMenuByEnum<T>(itemEnum.ToString());
        }
    }
}