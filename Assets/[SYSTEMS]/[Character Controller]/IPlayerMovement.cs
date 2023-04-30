using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    public interface IPlayerMovement
    {
        void Move(Vector3 targetPosition, float speed);
        void Rotate(Vector3 direction, float speed);
        void Stop();
        void StopRotation();
        void StopMovement();
    }
}