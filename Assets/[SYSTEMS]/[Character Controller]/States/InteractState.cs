using System.Collections.Generic;
using _SYSTEMS_._Interaction_System_.Abstract;
using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    public abstract class InteractState : State,ICanInteract<IUsable>
    {
        public LayerMask interactionLayerMask;
        public List<IUsable> Interactions = new List<IUsable>();
        
        public bool CanInteract()
        {
            return transform.GetInteractions<Transform>(transform.position,
                    2,interactionLayerMask).ContactCount > 0;
        }
        
        public void InteractionUpdate()
        {
            Debug.Log(CanInteract());
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(CanInteract() == false) return;
                
                foreach (var interaction in Interactions)
                    interaction.Use();
            }
        }
        
        public override void OnTick()
        {
            InteractionUpdate();
        }
    }
}