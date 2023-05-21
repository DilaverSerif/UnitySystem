using System;
using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._InventorySystem_.Interface;
using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/Scanner/Create Collector", fileName = "Collector", order = 0)]
    public class Collector : Scanner
    {
        public Bag bag;
        
        public override void OnSetup(ref StateMachine stateMachine)
        {
            base.OnSetup(ref stateMachine);
            bag = GetReference<Bag>("Inventory");
        }

        public override void OnTick()
        {
            var colliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(CenterPoint,
                scannerData.radius, colliders, scannerData.layerMask);

            foreach (var collider1 in colliders)
            {
                if (collider1 == null) continue;
                if (collider1.TryGetComponent(out ICollectable collectable))
                    collectable.Collect(bag.CurrentInventory);
            }
        }

        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR

            ExtensionMethods.DrawDisc(CenterPoint, scannerData.radius, Color.blue, 3);
            ExtensionMethods.DrawText(CenterPoint + Vector3.up, "Collector", Color.red);
#endif
        }
    }
}