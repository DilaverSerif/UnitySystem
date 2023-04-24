using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    public class PlayerController : MonoBehaviour
    {        
        // [ShowInInspector]
        // public IPlayerMovement PlayerMovement;
        public PlayerMovementData playerMovementData;
        public InputController InputController;

        public StateManager Manager { get; private set; }
        
        public float radius = 0.5f;
        public float distance = 1f;
        public LayerMask groundLayer;
        
        public Vector3[] FrontDirections = new Vector3[3];
        public float frontDistance = 1f;
        public float frontYOffset = 0.5f;

        public FrontHits FrontResults;

        private void Awake()
        {
            InputController = new InputController();
            // PlayerMovement = GetComponentInChildren<IPlayerMovement>();
            Manager = GetComponent<StateManager>();
        }

        private void FixedUpdate()
        {
            if (InputController == null)
                return;
            
            if (InputController.MovementDirection != Vector3.zero)
                Manager.TransitionToState(Manager.states[0]);
        }

        private void Update()
        {
            IsGrounded();
            IsFrontHit();
        }
        
        
        public bool IsGrounded()
        {
            var hit = new RaycastHit[1];
            var hits=  Physics.SphereCastNonAlloc(transform.position, radius, Vector3.down, hit, frontDistance, groundLayer);
            playerMovementData.isGrounded = hits > 0;
            playerMovementData.groundTransform = hit[0].transform;
            
            return hits > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, radius);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distance);
            
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + Vector3.up * frontYOffset, FrontDirections[0] * frontDistance);
            Gizmos.DrawRay(transform.position + Vector3.up * frontYOffset, FrontDirections[1] * frontDistance);
            Gizmos.DrawRay(transform.position + Vector3.up * frontYOffset, FrontDirections[2] * frontDistance);
            
        }
        
        public void IsFrontHit()
        {
            var hitResult = new FrontHits();
            RaycastHit hit;

            foreach (var dic in FrontDirections)
            {
                var hitCount = Physics.Raycast(transform.position + Vector3.up * frontYOffset, dic, out hit, 10f, groundLayer);
                
                
                
                if (dic == FrontDirections[0])
                {
                    hitResult.Left = hitCount;
                    playerMovementData.leftTransform = hit.transform;
                }
                else if (dic == FrontDirections[1])
                {
                    hitResult.Center = hitCount;
                    playerMovementData.centerTransform = hit.transform;
                }
                else if (dic == FrontDirections[2])
                {
                    hitResult.Right = hitCount;
                    playerMovementData.rightTransform = hit.transform;
                }
            }
            
            FrontResults = hitResult;
        }
        
        [System.Serializable]
        public struct FrontHits
        {
            public bool Left;
            public bool Right;
            public bool Center;
        }
    }
}
