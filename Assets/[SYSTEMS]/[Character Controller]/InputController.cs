

using UnityEngine;

namespace _SYSTEMS_._Character_Controller_
{
    public class InputController
    {
        public Vector3 MovementDirection
        {
            get
            {
                var x = Input.GetAxis("Horizontal");
                var z = Input.GetAxis("Vertical");
                return new Vector3(x, 0,z);
            }
        }
    }
}