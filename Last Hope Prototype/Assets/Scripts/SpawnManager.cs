using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;
    public SpawnPoint currrentSpawn;

    void Start()
    {
        if (spawnPoints.Length > 0)
        {
            SetupSpawnPoint(0);
        }
    }

    void Update()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].triggered)
            {
                SetupSpawnPoint(i);
            }
        }
    }

    void SetupSpawnPoint(int index)
    {
        currrentSpawn = spawnPoints[index];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].OnNewSpawnPoint();
        }
    }


    public Vector3 GetRespawnPoint()
    {
        return currrentSpawn.transform.position;
    }


}
