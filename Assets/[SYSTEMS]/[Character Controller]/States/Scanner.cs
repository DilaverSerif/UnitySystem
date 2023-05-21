using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    public abstract class Scanner : ScriptableObject, IReferenceContainer
    {
        public ScannerData scannerData;
        protected Transform transform;
        protected StateMachine _stateMachine;
        public Vector3 CenterPoint => scannerData.offsetPoint + transform.position;

        public virtual void OnSetup(ref StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            transform = _stateMachine.transform;
        }
        
        public abstract void OnTick();

        public virtual void OnDrawGizmos()
        {
        }

        public T GetReference<T>(string key)
        {
            return _stateMachine.GetReference<T>(key);
        }

        public void SetReference(string key, object value)
        {
            _stateMachine.SetReference(key, value);
        }
    }
}