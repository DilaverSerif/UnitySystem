using System;
using System.Collections.Generic;
using System.Linq;
using _SYSTEMS_.Extension;
using UnityEngine;
using UnityEngine.Events;

namespace _SYSTEMS_._Character_Controller_
{
    public interface IInputController
    {
        Vector3 MovementDirection();
        Vector3 RotationDirection();
    }

    public class InputController : Singleton<InputController>, IInputController
    {
        [Serializable]
        public struct KeyEvent
        {
            public KeyCode keyCode;
            public UnityEvent onKeyPressed;
        }
        
        public struct InputData
        {
            public float horizontal;
            public float vertical;
        }
        
        public List<KeyEvent> keyEvents = new List<KeyEvent>();

        public Vector3 MovementDirection()
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");
            return new Vector3(x, 0, z);
        }
        
        public Vector3 RotationDirection()
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");
            return new Vector3(x, y, 0);
        }

        private void Update()
        {
            if(keyEvents.Count == 0) return;

            foreach (var keyEvent in keyEvents.Where(keyEvent => Input.GetKeyDown(keyEvent.keyCode)))
            {
                keyEvent.onKeyPressed.Invoke();
            }
        }
    }
}