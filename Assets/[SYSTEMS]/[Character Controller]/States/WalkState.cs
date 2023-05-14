using System.Collections.Generic;
using _SYSTEMS_._Interaction_System_.Abstract;
using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/State/Create WalkState", fileName = "WalkState", order = 0)]
    public class WalkState : State, IPlayerMovement
    {
        private CharacterController _characterController;
        private Vector3 _targetPosition;
        private PlayerMovementData _playerMovementData;
        
        public List<InteractionBase<IUsable>> usableInteractions;
        public IUsable currentInteraction;

        public override void OnTick()
        {
            _targetPosition = GetReference<Vector3>("MoveDirection");

            Rotate();
            Move();
        }

        public override void OnEnter()
        {
            if(_playerMovementData != null) return;
            _playerMovementData = GetReference<PlayerMovementData>("MoveData");
            _characterController = GetReference<CharacterController>("CharacterController");
            _targetPosition = GetReference<Vector3>("MoveDirection");
        }

        public override void OnExit()
        {
            
        }
        
        private void Interaction()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(currentInteraction == null) return;
                
                foreach (var interaction in usableInteractions)
                {
                    interaction.TouchInteract(currentInteraction);
                }
            }
        }

        public override void OnFixedTick()
        {
            
        }

        public void Move()
        {
            var move = _targetPosition * (_playerMovementData.movementSpeed * Time.deltaTime);
            _characterController.Move(move);
        }

        public void Rotate()
        {
            var position = transform.position;
            var direction = ((_targetPosition + position) - position);
            direction.y = 0;
            
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
                _playerMovementData.rotationSpeed * Time.deltaTime);
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
