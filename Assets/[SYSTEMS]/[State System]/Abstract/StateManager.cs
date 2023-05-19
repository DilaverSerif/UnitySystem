using System.Collections.Generic;
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
                states[i].SetStateMachine(stateMachine);
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
            
            foreach (var state in states)
                state.Start();
        }

        private void Update()
        {
            if (currentState == null) 
                return;

            currentState.OnTick();
        }

        private void FixedUpdate()
        {
            if (currentState == null) 
                return;

            currentState.OnFixedTick();
        }

        public void TransitionToState(State nextState)
        {
            
            if (nextState == null)
            {
                Debug.LogWarning("Null state provided for transition.");
                return;
            }
            
            if(nextState == currentState) return;
            
            if(currentState != null)
                currentState.OnExit();
            
           
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
            
            TransitionToState(nextState);
        }
        
        private void SetState(State state)
        {
            currentState = state;
            currentState.OnEnter();
        }

        private void OnDrawGizmos()
        {
            if (currentState == null) 
                return;

            currentState.OnDrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (currentState == null) 
                return;

            currentState.OnDrawGizmosSelected();
        }
    }
}