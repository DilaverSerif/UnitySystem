using _SYSTEMS_._State_System_.Abstract;
using UnityEngine;

namespace _SYSTEMS_._Character_Controller_.States
{
    [CreateAssetMenu(menuName = "Character System/State/Create WalkState", fileName = "WalkState", order = 0)]
    public class WalkState : State, IPlayerMovement
    {
        public override void OnTick()
        {
            Move();
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }

        public override void OnFixedTick()
        {
            
        }

        public void Move()
        {
            var getPosition = GetReference<Vector3>("TargetPosition");
            var body = GetReference<Rigidbody>("Body");
            
            //Move to target position with ridigbody
            body.MovePosition(getPosition);    
        }

        public void Rotate(Vector3 direction, float speed)
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void StopRotation()
        {
            throw new System.NotImplementedException();
        }

        public void StopMovement()
        {
            throw new System.NotImplementedException();
        }
    }
}
