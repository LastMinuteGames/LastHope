using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour {
    public List<GameObject> spawnPoints;
    public EnemySpawnManager manager;

    private bool spawning = false;
    
	void Start () {
        
    }
	
	void Update () {
		if (spawning)
        {
            for (int i = 0; i < spawnPoints.Count; ++i)
            {
                if (spawnPoints[i].GetComponent<EnemySpawnPoint>().delay <= 0)
                {
                    manager.SpawnEnemy(spawnPoints[i].transform, spawnPoints[i].GetComponent<EnemySpawnPoint>().type);
                    Destroy(spawnPoints[i]);
                    spawnPoints.RemoveAt(i);
                } else
                {
                    spawnPoints[i].GetComponent<EnemySpawnPoint>().delay -= Time.deltaTime;
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        spawning = true;
    }
}
