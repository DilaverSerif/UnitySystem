using System.Collections.Generic;
using System.Linq;
using _SYSTEMS_._Interaction_System_.Abstract;
using _SYSTEMS_._State_System_.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _SYSTEMS_._Character_Controller_.States
{
    public abstract class InteractState : State,ICanInteract<IUsable>
    {
        public LayerMask interactionLayerMask;
        public List<IUsable> Interactions = new List<IUsable>();
        
        [Title("Collider Settings")]
        public float radius = 2;
        public Vector3 offsetPoint;
        
        public bool CanInteract()
        {
            var result = transform.GetInteractions<IUsable>(transform.position,
                2,interactionLayerMask);
            
            if (result.ContactCount == 0) return false;

            Interactions = transform.GetInteractions<IUsable>(transform.position + offsetPoint,
                radius, interactionLayerMask).Interactions.ToList();
            return true;
        }
        
        public void InteractionUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!CanInteract()) return;
                
                foreach (var interaction in Interactions)
                    interaction.Use();
            }
        }
        
        public override void OnTick()
        {
            InteractionUpdate();
        }
        
        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + offsetPoint, radius);
        }
    }
}