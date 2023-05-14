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
        
        public override void OnEnter()
        {
            if(_playerMovementData != null) return;
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
            // if (transform.position.GetDistanceTo(_targetPosition) > 0.1f)
            // {
            //     _targetPosition = GetReference<Vector3>("MoveDirection");
            //     Rotate();
            //     Move();
            // }
        }
        
        public void Move()
        {
            // var move = _targetPosition * (_playerMovementData.movementSpeed * Time.deltaTime);
            // _characterController.Move(move);
        }

        public void Rotate()
        {
            // var position = transform.position;
            // var direction = (_targetPosition + position) - position;
            // direction.y = 0;
            //
            // var targetRotation = Quaternion.LookRotation(direction);
            // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
            //     _playerMovementData.rotationSpeed * Time.deltaTime);
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