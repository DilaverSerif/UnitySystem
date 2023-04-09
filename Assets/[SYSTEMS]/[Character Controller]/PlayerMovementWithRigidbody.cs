using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    public class PlayerMovementWithRigidbody
    {
        private Rigidbody _rigidbody;

        public PlayerMovementWithRigidbody(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Move(Vector3 direction, float speed)
        {
            _rigidbody.velocity = direction * speed;
        }

        public void Rotate(Vector3 direction, float speed)
        {
            _rigidbody.angularVelocity = direction * speed;
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void StopRotation()
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }

        public void StopMovement()
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}