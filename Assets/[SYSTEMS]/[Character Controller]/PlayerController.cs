using _SYSTEMS_._State_System_.Abstract;
using KinematicCharacterController;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    public class PlayerController : MonoBehaviour,ICharacterController
    {
        public PlayerMovementData playerMovementData;
        public IRaycastChecker raycastChecker;

        private InputController _inputController;
        private StateManager _manager;
        
        private void Awake()
        {
            _manager = GetComponent<StateManager>();
            raycastChecker = GetComponentInChildren<IRaycastChecker>();
        }
        
        private void FixedUpdate()
        {
            if (InputController.Instance.MovementDirection().magnitude > 0)
                _manager.TransitionToState(_manager.states[1]);
            else 
                _manager.TransitionToState(_manager.states[0]);
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void BeforeCharacterUpdate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void PostGroundingUpdate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void AfterCharacterUpdate(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            throw new System.NotImplementedException();
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
            throw new System.NotImplementedException();
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            ref HitStabilityReport hitStabilityReport)
        {
            throw new System.NotImplementedException();
        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition,
            Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
            throw new System.NotImplementedException();
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {
            throw new System.NotImplementedException();
        }
    }
}
