using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;
using UnityEngine.Serialization;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/State/Create WalkState", fileName = "WalkState", order = 0)]
    public class WalkState : State, IPlayerMovement
    {
        private IPlayerMovement _movement;
        private InputController _inputController;
        private PlayerMovementData _playerMovementData;

        public IPlayerMovement Movement
        {
            get
            {
                if (_movement == null)
                 _movement = GetReference<IPlayerMovement>("Movement");

                return _movement;
            }
        }
        
        // public InputController InputController
        // {
        //     get
        //     {
        //         if (_inputController == null)
        //             _inputController = GetReference<InputController>("Input");
        //
        //         return _inputController;
        //     }
        // }
        
        public PlayerMovementData PlayerMovementData
        {
            get
            {
                if (_playerMovementData == null)
                    _playerMovementData = GetReference<PlayerMovementData>("MoveData");

                return _playerMovementData;
            }
        }
        
        
        public override void OnTick()
        {
            Movement.Move(InputController.Instance.MovementDirection(),PlayerMovementData.movementSpeed);
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }

        public override void OnFixedTick()
        {
            
        }

        public void Move(Vector3 targetPosition, float speed)
        {
            var getPosition = GetReference<Vector3>("TargetPosition");
            var body = GetReference<Rigidbody>("Body");
            
            //Move to target position with ridigbody
            body.MovePosition(getPosition);    
        }

        public void Rotate(Vector3 direction, float speed)
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void StopRotation()
        {
            throw new System.NotImplementedException();
        }

        public void StopMovement()
        {
            throw new System.NotImplementedException();
        }
    }
}
