using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    [CreateAssetMenu(menuName = "Character System/Create RayData", fileName = "RayData", order = 0)]
    public class RayData : ScriptableObject
    {
        public HitData[] rays;

        [Serializable]
        public class HitData
        {   
            [Title("Ray Data")]
            public HitDataStruct hitDataStruct;
            
            [Title("Debug Data")]
            public bool isHit;
            public Transform hitTransform;
        }
        
        [Serializable]
        public struct HitDataStruct
        {
            public Vector3 direction;
            public Vector3 offset;
            
            public LayerMask checkLayer;
            
            public float distance;
            public float radius;
        }
    }
}