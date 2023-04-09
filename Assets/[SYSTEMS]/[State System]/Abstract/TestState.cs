using UnityEngine;

namespace _SYSTEMS_._Character_System_.Abstract
{
    [CreateAssetMenu(menuName = "Create TestState", fileName = "TestState", order = 0)]
    public class TestState : State
    {
        public string testString;
        public override void OnEnter()
        {
            Debug.Log("OnEnter");
        }

        public override void OnExit()
        {
            Debug.Log("OnExit");
        }

        public override void OnTick()
        {
            Debug.Log(testString + GetReference<Transform>("Target").name);
        }
    }
}