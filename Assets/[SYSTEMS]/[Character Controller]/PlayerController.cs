using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovementData playerMovementData;
        public IRaycastChecker raycastChecker;

        private InputController _inputController;
        private StateManager _manager;
        
        private void Awake()
        {
            _manager = GetComponent<StateManager>();
            raycastChecker = GetComponentInChildren<IRaycastChecker>();
        }
        
        private void FixedUpdate()
        {
            if (InputController.Instance.MovementDirection().magnitude > 0)
                _manager.TransitionToState(_manager.states[1]);
            else 
                _manager.TransitionToState(_manager.states[0]);
        }
    }
}
