using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_.Extension
{
    public enum DebugColorEnum
    {
        Yellow,
        Red,
        Green,
        Blue,
        Black,
        White,
        Cyan,
        Magenta,
        Gray,
        Clear
    }

    public enum SystemsEnum
    {
        InventorySystem,
        MarketingSystem,
        CraftingSystem,
        QuestSystem,
        SaveSystem,
        PlayerSystem,
        EnemySystem,
        UISystem,
        CameraSystem,
        AudioSystem,
        AnimationSystem,
        InputSystem,
        MovementSystem,
        PhysicsSystem,
        GameSystem,
        ExtensionSystem,
        OtherSystem
    }

    [CreateAssetMenu(fileName = "DebugColor", menuName = "DebugColor", order = 0)]
    public class DebugColor : SingletonScriptableObject<DebugColor>
    {
        [Serializable]
        public struct DebugColorStruct
        {
            public SystemsEnum colorName;
            public DebugColorEnum colorEnum;

            [ReadOnly, ShowInInspector]
            public Color Color
            {
                get
                {
                    switch (colorEnum)
                    {
                        case DebugColorEnum.Yellow:
                            return Color.yellow;
                        case DebugColorEnum.Red:
                            return Color.red;
                        case DebugColorEnum.Green:
                            return Color.green;
                        case DebugColorEnum.Blue:
                            return Color.blue;
                        case DebugColorEnum.Black:
                            return Color.black;
                        case DebugColorEnum.White:
                            return Color.white;
                        case DebugColorEnum.Cyan:
                            return Color.cyan;
                        case DebugColorEnum.Magenta:
                            return Color.magenta;
                        case DebugColorEnum.Gray:
                            return Color.gray;
                        case DebugColorEnum.Clear:
                            return Color.clear;
                        default:
                            return Color.white;
                    }
                }
            }
        }

        public DebugColorStruct[] titleSystem;

        public string GetTitleSystem(SystemsEnum index)
        {
            foreach (var item in titleSystem)
            {
                if (item.colorName == index)
                {
                    return AddColor(item.colorName.ToString(), item.colorEnum);
                }
            }

            return AddColor("Other System", DebugColorEnum.White);
        }

        private string AddColor(string text, DebugColorEnum color)
        {
            return $"<color={color}>{text}</color>";
        }

        public void SystemLog(string text, SystemsEnum system)
        {
            Debug.Log($"{GetTitleSystem(system)}: {text}");
        }
    }

    public static class DebugColorExtension
    {
        public static void Log(this object message, SystemsEnum system)
        {
            Debug.Log($"{DebugColor.Instance.GetTitleSystem(system)}: {message}");
        }
    }
}