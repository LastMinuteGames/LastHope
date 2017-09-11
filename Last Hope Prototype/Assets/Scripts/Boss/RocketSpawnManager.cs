using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnManager : MonoBehaviour {

    public GameObject rocket;

    public GameObject SpawnRocket(RocketSpawnPoint point)
    {
        GameObject spawnedRocket = Instantiate(rocket, point.transform.position, point.transform.rotation);
        return spawnedRocket;
    }
}
