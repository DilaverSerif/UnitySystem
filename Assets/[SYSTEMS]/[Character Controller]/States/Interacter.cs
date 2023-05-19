using System.Collections.Generic;
using System.Linq;
using _SYSTEMS_._Interaction_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    public class Interacter
    {
        private readonly LayerMask _interactionLayerMask;
        private List<IUsable> _interactions = new List<IUsable>();
        private float _radius = 2;
        private Vector3 _offsetPoint;

        private Transform transform;

        public Interacter(Transform transform, float radius, LayerMask interactionLayerMask, Vector3 offsetPoint)
        {
            this._interactionLayerMask = interactionLayerMask;
            this._radius = radius;
            this._offsetPoint = offsetPoint;
            this.transform = transform;
        }
        
        public void SetRadius(float radius)
        {
            _radius = radius;
        }
        
        public void SetOffsetPoint(Vector3 offsetPoint)
        {
            _offsetPoint = offsetPoint;
        }
        
        public List<IUsable> GetInteractions()
        {
            return _interactions;
        }

        public bool CanInteract()
        {
            var result = transform.GetInteractions<IUsable>(transform.position,
                2, _interactionLayerMask);

            if (result.ContactCount == 0) return false;

            _interactions = transform.GetInteractions<IUsable>(transform.position + _offsetPoint,
                _radius, _interactionLayerMask).Interactions.ToList();
            return true;
        }

        public void OnTickInteraction()
        {
            if (!CanInteract()) return;

            foreach (var interaction in _interactions)
                interaction.Use();
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _offsetPoint, _radius);
        }

        public void OnDrawGizmos()
        {
#if UNITY_EDITOR

            ExtensionMethods.DrawDisc(transform.position + _offsetPoint,_radius,Color.green,3);
            ExtensionMethods.DrawText(transform.position + _offsetPoint,"Interacter",Color.red);
#endif
        }
    }
}