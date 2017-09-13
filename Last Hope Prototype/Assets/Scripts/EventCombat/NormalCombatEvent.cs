using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EnemySpawnSystem;

public class NormalCombatEvent : GenericCombatEvent
{

    override protected void InitData()
    {
        base.InitData();

        Wave wave = new Wave("1");
        wave.AddEnemy(EnemyType.ET_TRASH, 3, 5);

        waves.Add(wave);

        //wave = new Wave("2");
        //wave.AddEnemy(EnemyType.ET_TRASH, 5, 5);

        //waves.Add(wave);

        //wave = new Wave("3");
        //wave.AddEnemy(EnemyType.ET_TRASH, 7, 15);

        //waves.Add(wave);

        //WARNING!
        currentWave = waves[0];

        AddSpawnsToPendingEnemies(currentWave.StartWave());
        UnblockExits();
    }

    //protected override void EventStart()
    //{
    //    base.EventStart();
    //}

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

                //string text = "Nice job! But the Colossal wall is under attack!";
                //string from = "";
                //DialogueSystem.Instance.AddDialogue(text, from, 3.5f);
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
}
