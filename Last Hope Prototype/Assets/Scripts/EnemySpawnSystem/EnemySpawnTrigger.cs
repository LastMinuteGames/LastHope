using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour {
    public List<EnemySpawnPoint> spawnPoints;
    public EnemySpawnManager manager;

    private bool spawning = false;
    
	void Start () {
        
    }
	
	void Update () {
		if (spawning)
        {
            for (int i = 0; i < spawnPoints.Count; ++i)
            {
                if (spawnPoints[i].delay <= 0)
                {
                    manager.SpawnEnemy(spawnPoints[i]);
                    Destroy(spawnPoints[i]);
                    spawnPoints.RemoveAt(i);
                } else
                {
                    spawnPoints[i].delay -= Time.deltaTime;
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        spawning = true;
    }
}
