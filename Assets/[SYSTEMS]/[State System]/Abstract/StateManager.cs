using System.Collections.Generic;
using _SYSTEMS_._Character_System_.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._State_System_.Abstract
{
    [RequireComponent(typeof(StateMachine))]
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private State initialState;
        public List<State> states;

        [Title("Debug")]
        [SerializeField,ReadOnly]
        private StateMachine stateMachine;
        [SerializeField,ReadOnly]
        private State currentState;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();

            for (var i = 0; i < states.Count; i++)
            {
                states[i] = Instantiate(states[i]);
            }
            
            if (initialState == null)
            {
                Debug.LogWarning("No initial state provided for state machine.");
                initialState = states[0];
            }
            
        }

        private void Start()
        {
            TransitionToState(initialState);
        }

        private void Update()
        {
            if (currentState == null) 
                return;

            currentState.OnTick();
        }

        public void TransitionToState(State nextState)
        {
            if (nextState == null)
            {
                Debug.LogWarning("Null state provided for transition.");
                return;
            }
            
            if(currentState != null)
                currentState.OnExit();
            
            nextState.SetStateMachine(stateMachine);
            SetState(nextState);
        }
        
        public void TransitionToState(int nextStateIndex)
        {
            if (nextStateIndex < 0 || nextStateIndex >= states.Count)
            {
                Debug.LogWarning("Invalid state index provided for transition.");
                return;
            }
            
            var nextState = states[nextStateIndex];
            
            if (nextState == null)
            {
                Debug.LogWarning("Null state provided for transition.");
                return;
            }

            currentState.OnExit();
            nextState.SetStateMachine(stateMachine);
            SetState(nextState);
        }
        
        private void SetState(State state)
        {
            currentState = state;
            currentState.OnEnter();
        }
    }
}