using UnityEngine;

namespace _SYSTEMS_._Interaction_System_.Abstract
{
    public class UsableItem : MonoBehaviour,IUsable
    {
        public void Use()
        {
            gameObject.SetActive(false);
        }

        public void StopUse()
        {
            throw new System.NotImplementedException();
        }
    }
}