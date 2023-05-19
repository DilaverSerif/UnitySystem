using System;
using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._InventorySystem_.Interface;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [Serializable]
    public class Collector
    {
        public LayerMask layerMask;
        public float radius;
        public Vector3 offsetPoint;
        public Bag bag;
        
        public Collector(Bag bag, float radius, LayerMask layerMask,Vector3 offsetPoint)
        {
            this.layerMask = layerMask;
            this.radius = radius;
            this.offsetPoint = offsetPoint;
            this.bag = bag;
        }
        
        public void OnTickCollector()
        {
            var colliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(bag.transform.position + offsetPoint, radius, colliders, layerMask);

            foreach (var collider1 in colliders)
            {
                if(collider1 == null) continue;
                if (collider1.TryGetComponent(out ICollectable collectable))
                    collectable.Collect(bag.CurrentInventory);
            }

            //OnDrawGizmos();
        }
        
        public void OnDrawGizmos()
        {
#if UNITY_EDITOR

            ExtensionMethods.DrawDisc(bag.transform.position + offsetPoint,radius,Color.blue,3);
            ExtensionMethods.DrawText(bag.transform.position + Vector3.up,"Collector",Color.red);
#endif
        }
    }
}