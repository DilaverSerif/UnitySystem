using _SYSTEMS_.Extension;
using UnityEngine;

namespace _SYSTEMS_._Interaction_System_.Abstract
{
    public static class InteractionExtensions
    {
        public static InteractionData<T> GetInteractions<T>(this Transform CanInteract,
            Vector3 startPoint, float radius,LayerMask layerMask,int maxResult = 1) where T : Component
        {
            var results = new Collider[maxResult];
            Physics.OverlapSphereNonAlloc(startPoint, radius, results,layerMask);
            
            var dataResults = new InteractionData<T>();
            dataResults.SetInteractions(ref results);
            return dataResults;
        }
        
        public struct InteractionData<T> where T : Component
        {
            public T[] Interactions;
            public int ContactCount;
            
            internal void SetInteractions(ref Collider[] interactions)
            {
                if (interactions == null || interactions.Length == 0)
                {
                    Interactions = null;
                    ContactCount = 0;
                    return;
                }

                Interactions = new T[interactions.Length];
                
                for (var i = 0; i < Interactions.Length; i++)
                {
                    if (interactions[i].TryGetComponent(out T result))
                    {
                        Interactions[i] = result;
                        ContactCount++;
                    }
                    else
                    {
                        "Error: Interaction not found".Log(SystemsEnum.OtherSystem);
                        break;
                    }
                }
            }
        }
    }
}