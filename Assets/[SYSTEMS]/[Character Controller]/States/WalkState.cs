using _SYSTEMS_._InventorySystem_.Abstract;
using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/State/Create WalkState", fileName = "WalkState", order = 0)]
    public class WalkState : State, IPlayerMovement
    {
        private CharacterController _characterController;
        private PlayerMovementData _playerMovementData;
        private Vector3 _targetPosition;

        public Scanner[] collector;

        public override void Awake()
        {
            base.Awake();
            foreach (var scanner in collector)
            {
                scanner.OnSetup(ref _stateMachine);
            }
            
        }

        public override void OnTick()
        {
            _targetPosition = GetReference<Vector3>("MoveDirection");

            foreach (var scanner in collector)
                scanner.OnTick();

            Rotate();
            Move();
        }

        public override void OnEnter()
        {
            if (_playerMovementData != null)
                return;

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
            var move = transform.forward * (_playerMovementData.movementSpeed);
            move.y = _playerMovementData.Gravity;
            _characterController.Move(move * Time.deltaTime);
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

        public override void OnDrawGizmos()
        {
            foreach (var collect in collector)
            {
                collect.OnDrawGizmos();
            }
        }
    }
}