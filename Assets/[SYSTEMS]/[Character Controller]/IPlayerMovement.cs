using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    public interface IPlayerMovement
    {
        void Move();
        void Rotate(Vector3 direction, float speed);
        void Stop();
        void StopRotation();
        void StopMovement();
    }
}