using UnityEngine;
using UnityEngine.Serialization;

namespace _SYSTEMS_._Character_Controller_
{
    [CreateAssetMenu(menuName = "PlayerSystem/Create PlayerMovementData", fileName = "PlayerMovementData", order = 0)]
    public class PlayerMovementData : ScriptableObject
    {
        public float movementSpeed;
        public float jumpForce;
        public float rotationSpeed;
        
        public bool isGrounded;
        public Transform groundTransform;
        
        public Transform leftTransform;
        public Transform rightTransform;
        public Transform centerTransform;
    }
}