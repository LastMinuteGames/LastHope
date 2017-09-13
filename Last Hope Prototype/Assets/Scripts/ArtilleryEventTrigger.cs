using Assets.Scripts.EnemySpawnSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArtilleryEventTrigger : GenericCombatEvent
{

    [SerializeField]
    private GameObject artilleryCamera;

    override protected void InitData()
    {
        base.InitData();

        Wave wave = new Wave("1");
        wave.AddEnemy(EnemyType.ET_TRASH, 3, 5);

        waves.Add(wave);

        wave = new Wave("2");
        wave.AddEnemy(EnemyType.ET_TRASH, 5, 5);

        waves.Add(wave);

        wave = new Wave("3");
        wave.AddEnemy(EnemyType.ET_TRASH, 7, 15);

        waves.Add(wave);

        //WARNING!
        currentWave = waves[0];

        AddSpawnsToPendingEnemies(currentWave.StartWave());
        UnblockExits();
    }

    protected override void EventStart()
    {
        base.EventStart();
        artilleryCamera.GetComponent<Camera>().enabled = true;
        artilleryCamera.GetComponent<Animator>().SetTrigger("Move");
    }

    override protected void UpdateEvent()
    {
        if (currentWave.IsFinished()) //Next wave!
        {
            //currentWave.FinishDebug();
            waves.RemoveAt(0);
            if (waves.Count > 0)
            {
                currentWave = waves[0];
                List<Spawn> spawns = currentWave.StartWave();
                //Debug.Log("Spawns Start Wave: " + spawns.Count);
                AddSpawnsToPendingEnemies(spawns);
            }
            else
            {
                currentWave = null;
                status = EVENT_STATUS.FINISHED;
                target.hpSlider.gameObject.SetActive(false);

                UnblockExits();

                string text = "Nice job! But the Colossal wall is under attack!";
                string from = "";
                DialogueSystem.Instance.AddDialogue(text, from, 3.5f);
            }
        }
        else //Update Waves!!!
        {
            Dictionary<EnemyType, uint> deadEnemies = CleanUpEnemies();
            AddSpawnsToPendingEnemies(currentWave.RemoveEnemies(deadEnemies));
            float seconds = Time.realtimeSinceStartup;
            List<EnemyType> keys = new List<EnemyType>(enemiesPendingToSpawn.Keys);
            for (int i = 0; i < keys.Count; ++i)
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

    //void Start () {
    //    artillery = transform.GetComponentInParent<ArtilleryController>();

    //    foreach (Transform child in reusableSpawnPointsParent.transform)
    //    {
    //        reusableSpawnPoints.Add(child.GetComponent<EnemySpawnPoint>());
    //    }

    //    foreach(Transform child in parentWallsEvent.transform)
    //    {
    //        GameObject eventWall = child.gameObject;
    //        eventWall.SetActive(false);
    //        eventWalls.Add(eventWall);
    //    }

    //    InitData();
    //}

    //void Update()
    //{
    //    if (isFinished == true || isStarted == false || currentWave == null)
    //        return;

    //    if(player != null && player.IsDead())
    //    {
    //        InitData();
    //    }

    //    if (artillery == null || (artillery != null && artillery.alive))
    //    {
    //        if (currentWave.IsFinished()) //Next wave!
    //        {
    //            //currentWave.FinishDebug();
    //            waves.RemoveAt(0);
    //            if(waves.Count > 0)
    //            {
    //                currentWave = waves[0];
    //                List<Spawn> spawns = currentWave.StartWave();
    //                //Debug.Log("Spawns Start Wave: " + spawns.Count);
    //                AddSpawnsToPendingEnemies(spawns);
    //            }
    //            else
    //            {
    //                currentWave = null;
    //                isFinished = true;
    //                artillery.hpSlider.gameObject.SetActive(false);

    //                UnblockExits();

    //                string text = "Nice job! But the Colossal wall is under attack!";
    //                string from = "";
    //                DialogueSystem.Instance.AddDialogue(text, from, 3.5f);
    //            }
    //        }
    //        else //Update Waves!!!
    //        {
    //            Dictionary<EnemyType, uint> deadEnemies = CleanUpEnemies();
    //            AddSpawnsToPendingEnemies(currentWave.RemoveEnemies(deadEnemies));
    //            float seconds = Time.realtimeSinceStartup;
    //            List<EnemyType> keys = new List<EnemyType>(enemiesPendingToSpawn.Keys);
    //            for(int i = 0; i < keys.Count; ++i)
    //            {
    //                EnemyType type = keys[i];
    //                if (enemiesPendingToSpawn[type] > 0 && (lastSpawnTime == 0 || seconds - lastSpawnTime > delayBetweenSpawns))
    //                {
    //                    enemiesPendingToSpawn[type]--;
    //                    SpawnEnemyType(type);
    //                    lastSpawnTime = seconds;
    //                }
    //            }
    //        }
    //    }
    //    else if(artillery.alive == false && artillery.gameObject.activeSelf == true)
    //    {
    //        //Destroy(artillery.gameObject);
    //        //artillery = null;
    //        //artillery.gameObject.SetActive(false);
    //        if (player != null)
    //            player.Respawn();
    //        InitData();

    //        //Debug.Log("Artillery destroyed. You lose");
    //        //TODO: Go to screen title? restart from last point?
    //    }
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        if (isStarted == false)
    //        {
    //            isStarted = true;
    //            artillery.hpSlider.gameObject.SetActive(true);

    //            artillery.alive = true;
    //            Debug.Log("Artillery event started");
    //            BlockExits();
    //            player = other.gameObject.GetComponent<PlayerController>();

    //            artilleryCamera.GetComponent<Camera>().enabled = true;
    //            artilleryCamera.GetComponent<Animator>().SetTrigger("Move");
    //        }
    //    }
    //}

    //private bool EventIsFinished()
    //{
    //    if (target != null && target.alive)
    //        return false;       
    //    else if (waves.Count > 0)
    //        return false;
    //    return true;
    //}

    //public void BlockExits()
    //{
    //    foreach(GameObject eventWall in eventWalls)
    //    {
    //        eventWall.SetActive(true);
    //    }
    //}

    //public void UnblockExits()
    //{
    //    foreach (GameObject eventWall in eventWalls)
    //    {
    //        eventWall.SetActive(false);
    //    }
    //}

    //Dictionary<EnemyType, uint> CleanUpEnemies()
    //{
    //    Dictionary<EnemyType, uint> result = new Dictionary<EnemyType, uint>();
    //    int before = enemies.Count;
    //    for (int i = 0; i < enemies.Count; ++i)
    //    {
    //        if (enemies[i].IsDead() && enemies[i].Autokill == true)
    //        {
    //            //enemies[i].Autokill = true;
    //            if (result.ContainsKey(enemies[i].enemyType) == false)
    //                result[enemies[i].enemyType] = 0;
    //            enemies[i].Dead();
    //            result[enemies[i].enemyType]++;
    //            //Destroy(enemies[i].gameObject);
    //            enemies.RemoveAt(i);
    //            --i;
    //        }
    //    }

    //    return result;
    //}

    //private void AddSpawnsToPendingEnemies(List<Spawn> spawns)
    //{
    //    if(spawns.Count > 0)
    //    {
    //        foreach (Spawn spawn in spawns)
    //        {
    //            if (enemiesPendingToSpawn.ContainsKey(spawn.type))
    //            {
    //                enemiesPendingToSpawn[spawn.type] += spawn.number;
    //            }
    //            else
    //            {
    //                enemiesPendingToSpawn[spawn.type] = spawn.number;
    //            }
    //        }
    //    }
    //}

    //private void SpawnEnemyType(EnemyType type)
    //{
    //    int index = (int)UnityEngine.Random.Range(0, reusableSpawnPoints.Count - 1);
    //    manager.SpawnEnemy(reusableSpawnPoints[index], type, this);        
    //}

    //public void AddEnemy(Enemy enemy)
    //{
    //    if (enemy != null)
    //    {
    //        enemy.Autokill = false;
    //        enemy.behaviour = EnemyBehaviour.EB_ARTILLERY;
    //        enemies.Add(enemy);
    //    }
    //}
}
