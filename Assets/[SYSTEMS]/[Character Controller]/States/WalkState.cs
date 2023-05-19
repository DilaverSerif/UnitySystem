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
        
        private Collector _collector;
        private Interacter _interacter;

        public override void Start()
        {
            _collector = new Collector(GetReference<Bag>("Inventory"), 2f, LayerMask.GetMask("Collectable"),
                Vector3.zero);
            _interacter = new Interacter(transform, 2f, LayerMask.GetMask("Interact"), Vector3.zero);
        }

        public override void OnTick()
        {
            _targetPosition = GetReference<Vector3>("MoveDirection");
            _collector.OnTickCollector();
            _interacter.OnTickInteraction();
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
            var move = transform.forward * (_playerMovementData.movementSpeed * Time.deltaTime);
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

        public override void OnDrawGizmos()
        {
            if(_collector != null)
                _collector.OnDrawGizmos();
            if(_interacter != null)
                _interacter.OnDrawGizmos();
        }
    }
}