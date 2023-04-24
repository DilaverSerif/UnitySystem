using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts._SYSTEMS_._Character_System_.Interface;
using UnityEngine;

namespace _SYSTEMS_._State_System_.Abstract
{
    public interface IReferenceContainer
    {
        T GetReference<T>(string key);
        void SetReference(string key, object value);
    }

    public abstract class State : ScriptableObject, IReferenceContainer,IState
    {
        private StateMachine _stateMachine;
        protected Transform transform;
        
        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            transform = _stateMachine.transform;
        }

        public T GetReference<T>(string key)
        {
            return _stateMachine.GetReference<T>(key);
        }

        public void SetReference(string key, object value)
        {
            _stateMachine.SetReference(key, value);
        }
        
        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _stateMachine.StartCoroutine(coroutine);
        }

        public abstract void OnTick();
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}