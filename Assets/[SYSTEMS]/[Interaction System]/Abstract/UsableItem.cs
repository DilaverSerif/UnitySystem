using UnityEngine;

namespace _SYSTEMS_._Interaction_System_.Abstract
{
    public class UsableItem : MonoBehaviour,IUsable<GameObject>
    {
        public void Use(GameObject target)
        {
            gameObject.SetActive(false);
        }

        public void StopUse(GameObject target)
        {
            throw new System.NotImplementedException();
        }
    }
}