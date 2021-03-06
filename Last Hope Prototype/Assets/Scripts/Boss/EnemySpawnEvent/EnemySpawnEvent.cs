﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/SpawnEvent")]
public class EnemySpawnEvent : BossEvent
{
    //EnmyspawnPoints[] seria mejor pero necesitariamos prefabs de enemyspawnpoints. de todas formas tenemos 1 solo enemigo de momento
    public List<EnemySpawnPoint> spawnPoints;
    private EnemySpawnManager manager;
    private TurretsManager turrets;

    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting spawnEvent");
        manager = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        turrets = GameObject.Find("Turrets").GetComponent<TurretsManager>();

 
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            spawnPoints[i].done = false;
            spawnPoints[i].delay = spawnPoints[i].initialDelay;
        }

		BossManager.instance.SetEmisiveYellow();
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Boss_Spawn_Attack);
    }

    public override bool UpdateEvent()
    {
        Debug.Log("update spawnEvent");

        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            if (spawnPoints[i].delay <= 0 && !spawnPoints[i].done)
            {
                CapsuleController aux = manager.SpawnEnemy(spawnPoints[i]).GetComponent<CapsuleController>();
                turrets.capsules.Add(aux);
                spawnPoints[i].done = true;
                spawnPoints[i].delay = spawnPoints[i].initialDelay;
                //Destroy(spawnPoints[i]);
                //spawnPoints.RemoveAt(i);
            }
            else
            {
                spawnPoints[i].delay -= Time.deltaTime;
            }
        }

        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating spawnEvent");
    }
}
