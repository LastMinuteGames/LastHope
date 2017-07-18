using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/RocketRainEvent")]
public class RocketRainEvent : BossEvent
{
    public List<RocketSpawnPoint> spawnPoints;
    private RocketSpawnManager manager;

    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting RocketRainEvent");
        manager = GameObject.Find("RocketSpawnManager").GetComponent<RocketSpawnManager>();
        if (manager == null) TerminateEvent();
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            spawnPoints[i].done = false;
            spawnPoints[i].delay = spawnPoints[i].initialDelay;
        }
    }

    public override bool UpdateEvent()
    {
        //Debug.Log("update RocketRainEvent");
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            if (spawnPoints[i].delay <= 0 && !spawnPoints[i].done)
            {
                manager.SpawnRocket(spawnPoints[i]);
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
        Debug.Log("terminating RocketRainEvent");
    }
}
