using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/State/Create New State", fileName = "Idle State", order = 0)]
    public class IdleState : State, IPlayerMovement
    {
        private CharacterController _characterController;
        private Vector3 _targetPosition;
        private PlayerMovementData _playerMovementData;

        private Vector3 _locomotionVelocity;
        public float locomotionDistance = 1;

        public override void OnEnter()
        {
            _locomotionVelocity = transform.forward * locomotionDistance;

            if (_playerMovementData != null) return;
            _playerMovementData = GetReference<PlayerMovementData>("MoveData");
            _characterController = GetReference<CharacterController>("CharacterController");
        }

        public override void OnExit()
        {
        }

        public override void OnFixedTick()
        {
        }

        public override void OnTick()
        {
            if (_locomotionVelocity.magnitude <= 0) return;
            _locomotionVelocity = Vector3.MoveTowards(_locomotionVelocity, Vector3.zero,
                _playerMovementData.movementSpeed * Time.deltaTime);
            _characterController.Move(_locomotionVelocity * Time.deltaTime);
        }

        public void Move()
        {
        }

        public void Rotate()
        {
        }

        public void Stop()
        {
        }

        public void StopRotation()
        {
        }

        public void StopMovement()
        {
        }
    }
}

