using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/Scanner/Create ScannerData", fileName = "ScannerData", order = 0)]
    public class ScannerData : ScriptableObject
    {
        public LayerMask layerMask;
        public float radius;
        public Vector3 offsetPoint;
    }
}