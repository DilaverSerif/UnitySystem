using System.Collections.Generic;
using UnityEngine;

namespace _SYSTEMS_._Spawner_System_
{
    public class SpawnerSystem : MonoBehaviour
    {
        public List<Spawner> Spawners;
        
        private void Start()
        {
            foreach (var spawner in Spawners)
            {
                StartCoroutine(spawner.Spawn());
            }
        }
        
        private void OnDrawGizmos()
        {
            foreach (var spawner in Spawners)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, spawner.Radius);
            }
        }
        
    }
}
