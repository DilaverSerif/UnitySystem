using System;
using _SYSTEMS_._Interaction_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [Serializable]
    public class Collector<T>
    {
        public LayerMask layerMask;
        public float radius;
        public Vector3 offsetPoint;
        public T bag;
        public Transform transform;

        public Collector(T bag, Transform transform, float radius, LayerMask layerMask, Vector3 offsetPoint)
        {
            this.layerMask = layerMask;
            this.radius = radius;
            this.offsetPoint = offsetPoint;
            this.bag = bag;
            this.transform = transform;
        }

        public void OnTickCollector()
        {
            var colliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(transform.position + offsetPoint, radius, colliders, layerMask);
            if (colliders[0] == null)
                return;
            
            foreach (var collider1 in colliders)
            {
                if (collider1.TryGetComponent(out IUsable<T> collectable))
                    collectable.Use(bag);
            }
        }

        public void OnDrawGizmos()
        {
#if UNITY_EDITOR

            ExtensionMethods.DrawDisc(transform.position + offsetPoint, radius, Color.blue, 3);
            ExtensionMethods.DrawText(transform.position + Vector3.up, "Collector", Color.red);
#endif
        }
    }
}