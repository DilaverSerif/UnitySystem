using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _SYSTEMS_._Spawner_System_
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Spawner System/Create Spawner")]
    public class Spawner : ScriptableObject
    {
        public SpawnPool[] spawnPool;
        public int Radius;
        [ReadOnly] public int SpawnCount => SpawnedObjects.Count;
        public Vector2 SpawnDelay;
        public float SpawnMax;
        public bool IsSpawned;
        public bool CanSpawn => SpawnCount < SpawnMax;
        [ReadOnly] public List<GameObject> SpawnedObjects;

        public IEnumerator Spawn()
        {
            while (CanSpawn)
            {
                SpawnedObjects.Add(Instantiate(
                    spawnPool[Random.Range(0, spawnPool.Length)].prefab[Random.Range(0, spawnPool.Length)],
                    Random.insideUnitSphere * Radius, Quaternion.identity));
                yield return new WaitForSeconds(Random.Range(SpawnDelay.x, SpawnDelay.y));
            }
        }
    }
}