using System.Collections;
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
            Awake();
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
        
        protected void StopCoroutine(Coroutine coroutine)
        {
            _stateMachine.StopCoroutine(coroutine);
        }
        
        public virtual void OnDrawGizmosSelected()
        {
            
        }
        
        public virtual void OnDrawGizmos()
        {
            
        }

        public abstract void OnTick();
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnFixedTick();
        public virtual void Awake()
        {
            
        }
        public virtual void Start()
        {
            
        }
    }
}