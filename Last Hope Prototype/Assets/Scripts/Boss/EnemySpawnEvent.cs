using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/SpawnEvent")]
public class EnemySpawnEvent : BossEvent
{
    public EnemySpawnPoint[] spawnPoints;

    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting spawnEvent");
    }

    public override bool UpdateEvent()
    {
        Debug.Log("update spawnEvent");
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating spawnEvent");
    }
}
