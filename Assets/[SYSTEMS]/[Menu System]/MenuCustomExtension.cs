using UnityEditor;
using UnityEngine;

namespace _SYSTEMS_._Menu_System_
{
    public static class MenuCustomExtension
    {
        [MenuItem("GameObject/UI/Base Button",false,3)]
        static void AddBaseButton(MenuCommand menuCommand)
        {
            var baseButton = Resources.Load<GameObject>("BaseButton");
            if(baseButton == null)
                return;
            
            var button = PrefabUtility.InstantiatePrefab(baseButton) as GameObject;
            if(button == null)
                return;
            
            button.transform.SetParent(Selection.activeGameObject.transform,false);
        }
        
        [MenuItem("GameObject/UI/Base Text",false,2)]
        static void AddBaseText(MenuCommand menuCommand)
        {
            var baseText = Resources.Load<GameObject>("BaseText");
            if(baseText == null)
                return;
            
            var text = PrefabUtility.InstantiatePrefab(baseText) as GameObject;
            if(text == null)
                return;
            
            text.transform.SetParent(Selection.activeGameObject.transform,false);
        }
        
        [MenuItem("GameObject/UI/Base Image",false,1)]
        static void AddBaseImage(MenuCommand menuCommand)
        {
            var baseImage = Resources.Load<GameObject>("BaseImage");
            if(baseImage == null)
                return;
            
            var image = PrefabUtility.InstantiatePrefab(baseImage) as GameObject;
            if(image == null)
                return;
            
            image.transform.SetParent(Selection.activeGameObject.transform,false);
        }
        
        [MenuItem("GameObject/UI/Base Menu",false,0)]
        static void AddBaseMenu(MenuCommand menuCommand)
        {
            var baseImage = Resources.Load<GameObject>("BaseMenu");
            if(baseImage == null)
                return;
            
            var image = PrefabUtility.InstantiatePrefab(baseImage) as GameObject;
            if(image == null)
                return;
            
            image.transform.SetParent(Selection.activeGameObject.transform,false);
        }
    }
}