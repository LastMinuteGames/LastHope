using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugSpawn : MonoBehaviour
{
    public PlayerDebugSpawnPoint spawnPoint;
    public Transform[] spawnPoints;

    private PlayerDebugSpawnPoint oldSpawnPoint;
    private Transform playerT;

    private void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateSpawn();
    }

    private void UpdateSpawn()
    {
        oldSpawnPoint = spawnPoint;
        int i = (int)spawnPoint;
        playerT.position = spawnPoints[i].position;

        if (spawnPoint == PlayerDebugSpawnPoint.BossFight)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCameraManager>().SetBossCam();
        }
    }

    private void Update()
    {
        if (spawnPoint != oldSpawnPoint)
        {
            UpdateSpawn();
        }
    }
}


public enum PlayerDebugSpawnPoint
{
    Start,
    Generator,
    BeforeArtillery,
    AfterArtillery,
    BeforeBoss,
    BossFight
}