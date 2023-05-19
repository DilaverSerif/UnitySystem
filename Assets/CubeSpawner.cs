using System.Collections.Generic;
using _SYSTEMS_._Character_Controller_.States;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public Vector2 spawnInterval;
    public List<Transform> cubes = new List<Transform>();
    private CalculateDistance _calculateDistance;
    private void Awake()
    {
        for (int i = 0; i < spawnInterval.x; i++)
        {
            for (int j = 0; j < spawnInterval.y; j++)
            {
                var spawnedCube = Instantiate(cubePrefab, new Vector3(i, j, 0), Quaternion.identity);
                cubes.Add(spawnedCube.transform);
            }
        }
    }

    private void Update()
    {
        _calculateDistance = new CalculateDistance()
        {
            distances = new NativeArray<Vector3>(cubes.Count, Allocator.TempJob),
            target = transform.position
        };

        var jobHandle = _calculateDistance.Schedule();
        jobHandle.Complete();
    }
    
    public struct CalculateDistance: IJob
    {
        public NativeArray<Vector3> distances;
        public Vector3 target;
        public NativeArray<int> acilacaklarIndex;
        public NativeArray<int> kapanacaklarIndex;
        
        public void Execute()
        {
            acilacaklarIndex = new NativeArray<int>(distances.Length, Allocator.TempJob);
            kapanacaklarIndex = new NativeArray<int>(distances.Length, Allocator.TempJob);
            
            for (int i = 0; i < distances.Length; i++)
            {
                var dis = target.GetDistanceTo(distances[i]);
                if(dis < 2)
                    acilacaklarIndex[i] = i;
                else 
                    kapanacaklarIndex[i] = i;
            }
        }
    }
}
