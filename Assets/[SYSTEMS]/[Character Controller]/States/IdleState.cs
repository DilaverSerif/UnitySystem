using System.Collections;
using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/State/Create New State", fileName = "Idle State", order = 0)]
    public class IdleState : State, IPlayerMovement
    {
        private Coroutine _locomotionIdle;
        public override void OnEnter()
        {
            // if (_locomotionIdle == null)
            //     _locomotionIdle = StartCoroutine(LocomotionIdle());
        }

        public override void OnExit()
        {
            _locomotionIdle = null;
        }

        public override void OnFixedTick()
        {
            
        }

        public override void OnTick()
        {
            
        }
        
        private IEnumerator LocomotionIdle()
        {
            var body = GetReference<Rigidbody>("Body");
            var data = GetReference<PlayerMovementData>("MoveData");
            var targetPosition = GetReference<Vector3>("TargetPosition");

            while (transform.position.GetDistanceTo(targetPosition) > 0.1f)
            {
                var direction = (targetPosition - transform.position).normalized;
                
                Rotate(direction, data.rotationSpeed);
                body.velocity = transform.forward * data.movementSpeed;
                yield return null;
            }
            
            _locomotionIdle = null;
        }

        public void Move()
        {
            
        }

        public void Rotate(Vector3 direction, float speed)
        {
            direction.y = transform.position.y;
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
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