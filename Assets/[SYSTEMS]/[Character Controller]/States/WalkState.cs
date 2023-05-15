using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/State/Create WalkState", fileName = "WalkState", order = 0)]
    public class WalkState : InteractState, IPlayerMovement
    {
        private CharacterController _characterController;
        private Vector3 _targetPosition;
        private PlayerMovementData _playerMovementData;
        
        public override void OnTick()
        {
            base.OnTick();
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
