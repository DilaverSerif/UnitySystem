using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    [Serializable]
    public class PlayerMovementWithRigidbody : MonoBehaviour, IPlayerMovement
    {
        public Rigidbody rigidBody;
        public Vector3 targetDirection;

        [ShowInInspector]
        public float CurrentSpeed
        {
            get
            {
                if (rigidBody == null)
                    return 0;
                
                return rigidBody.velocity.magnitude;
            }
        }
        
        public void Move(Vector3 targetPosition, float speed)
        {
            targetDirection = (transform.position - targetPosition).normalized;
            rigidBody.velocity = targetDirection * (speed * Time.deltaTime);
        }

        public void Rotate(Vector3 direction, float speed)
        {
            rigidBody.angularVelocity = direction * speed;
        }

        public void Stop()
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }

        public void StopRotation()
        {
            rigidBody.angularVelocity = Vector3.zero;
        }

        public void StopMovement()
        {
            rigidBody.velocity = Vector3.zero;
        }
    }
}