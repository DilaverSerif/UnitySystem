using System.Collections.Generic;
using System.Linq;
using _SYSTEMS_._Interaction_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/Scanner/Create Interacter", fileName = "Interacter", order = 0)]
    public class Interacter : Scanner
    {
        private List<IUsable> _interactions = new List<IUsable>();
        public List<IUsable> GetInteractions()
        {
            return _interactions;
        }

        private bool CanInteract()
        {
            var result = transform.GetInteractions<IUsable>(CenterPoint,
                scannerData.radius, scannerData.layerMask);

            if (result.ContactCount == 0) return false;

            _interactions = transform.GetInteractions<IUsable>(CenterPoint,
                scannerData.radius, scannerData.layerMask).Interactions.ToList();
            return true;
        }

        public override void OnTick()
        {
            Debug.Log(_interactions.Count);
            if (!(CanInteract())) return;
            Debug.Log("Interact");
            foreach (var interaction in _interactions)
                interaction.Use();
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CenterPoint, scannerData.radius);
        }

        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR

            ExtensionMethods.DrawDisc(CenterPoint, scannerData.radius, Color.green, 3);
            ExtensionMethods.DrawText(CenterPoint, "Interacter", Color.red);
#endif
        }
    }
}