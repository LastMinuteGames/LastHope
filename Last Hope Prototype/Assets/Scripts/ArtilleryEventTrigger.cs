using Assets.Scripts.EnemySpawnSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArtilleryEventTrigger : MonoBehaviour, EnemyObserver
{
    private ArtilleryController artillery;
    public GameObject blockExit1;
    public GameObject blockExit2;
    public EnemySpawnManager manager;
    public GameObject reusableSpawnPointsParent;
    public float delayBetweenSpawns = 2.0f;

    private List<EnemySpawnPoint> reusableSpawnPoints = new List<EnemySpawnPoint>();
    private List<Wave> waves = new List<Wave>();
    private Wave currentWave;
    private Dictionary<EnemyType, uint> enemiesPendingToSpawn = new Dictionary<EnemyType, uint>();
    private List<Enemy> enemies = new List<Enemy>();
    private float lastSpawnTime = 0;
    private bool isStarted = false;
    private bool isFinished = false;

    void Start () {
        artillery = transform.GetComponentInParent<ArtilleryController>();

        Wave wave = new Wave();
        wave.AddEnemy(EnemyType.ET_TRASH, 3, 5);

        waves.Add(wave);

        wave = new Wave();
        wave.AddEnemy(EnemyType.ET_TRASH, 5, 5);

        waves.Add(wave);

        wave = new Wave();
        wave.AddEnemy(EnemyType.ET_TRASH, 7, 15);

        waves.Add(wave);

        //WARNING!
        currentWave = waves[0];
        AddSpawnsToPendingEnemies(currentWave.StartWave());

        foreach (Transform child in reusableSpawnPointsParent.transform)
        {
            reusableSpawnPoints.Add(child.GetComponent<EnemySpawnPoint>());
        }

        if (blockExit1 != null && blockExit2 != null)
        {

            blockExit1.SetActive(false);
            blockExit2.SetActive(false);

        }
    }

    void Update()
    {
        if (isFinished == true || isStarted == false || currentWave == null)
            return;

        if (artillery != null && artillery.alive)
        {
            if (currentWave.IsFinished()) //Next wave!
            {
                waves.RemoveAt(0);
                if(waves.Count > 0)
                {
                    currentWave = waves[0];
                    List<Spawn> spawns = currentWave.StartWave();
                    AddSpawnsToPendingEnemies(spawns);
                }
                else
                {
                    currentWave = null;
                    isFinished = true;
                    UnblockExits();
                }
            }
            else //Update Waves!!!
            {
                Dictionary<EnemyType, uint> deadEnemies = CleanUpEnemies();
                AddSpawnsToPendingEnemies(currentWave.RemoveEnemies(deadEnemies));
                float seconds = Time.realtimeSinceStartup;
                List<EnemyType> keys = new List<EnemyType>(enemiesPendingToSpawn.Keys);
                for(int i = 0; i < keys.Count; ++i)
                {
                    EnemyType type = keys[i];
                    if (enemiesPendingToSpawn[type] > 0 && (lastSpawnTime == 0 || seconds - lastSpawnTime > delayBetweenSpawns))
                    {
                        enemiesPendingToSpawn[type]--;
                        SpawnEnemyType(type);
                        lastSpawnTime = seconds;
                    }
                }
            }
        }
        else
        {
            if(artillery != null)
            {
                Destroy(artillery.gameObject);
                artillery = null;
                Debug.Log("Artillery destroyed. You lose");
                //TODO: Go to screen title? restart from last point?
            }
        }
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isStarted == false)
            {
                isStarted = true;
                artillery.alive = true;
                Debug.Log("Artillery event started");
                BlockExits();
            }
        }
    }

    public void BlockExits()
    {
        if (blockExit1 != null && blockExit2 != null)
        {
            blockExit1.SetActive(true);
            blockExit2.SetActive(true);
        }
    }

    public void UnblockExits()
    {
        if (blockExit1 != null && blockExit2 != null)
        {
            Destroy(blockExit1);
            Destroy(blockExit2);
        }
    }

    Dictionary<EnemyType, uint> CleanUpEnemies()
    {
        Dictionary<EnemyType, uint> result = new Dictionary<EnemyType, uint>();
        for (int i = 0; i < enemies.Count; ++i)
        {
            if (enemies[i].IsDead() && enemies[i].Autokill == true)
            {
                //enemies[i].Autokill = true;
                if (result.ContainsKey(enemies[i].enemyType) == false)
                    result[enemies[i].enemyType] = 0;
                enemies[i].Dead();
                //result[enemies[i].enemyType]++;
                //Destroy(enemies[i].gameObject);
                enemies.RemoveAt(i);
                --i;
            }
        }

        return result;
    }

    private void AddSpawnsToPendingEnemies(List<Spawn> spawns)
    {
        if(spawns.Count > 0)
        {
            foreach (Spawn spawn in spawns)
            {
                if (enemiesPendingToSpawn.ContainsKey(spawn.type))
                {
                    enemiesPendingToSpawn[spawn.type] += spawn.number;
                }
                else
                {
                    enemiesPendingToSpawn[spawn.type] = spawn.number;
                }
            }
        }
    }

    private void SpawnEnemyType(EnemyType type)
    {
        //TODO: Maybe the nearest to the player?
        int index = (int)UnityEngine.Random.Range(0, reusableSpawnPoints.Count - 1);
        manager.SpawnEnemy(reusableSpawnPoints[index], type, this);
        
    }

    public void AddEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.Autokill = false;
            enemy.behaviour = EnemyBehaviour.EB_ARTILLERY;
            enemies.Add(enemy);
        }
    }
}
