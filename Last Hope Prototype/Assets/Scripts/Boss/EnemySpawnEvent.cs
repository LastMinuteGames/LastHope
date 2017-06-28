using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/SpawnEvent")]
public class EnemySpawnEvent : BossEvent
{
    //EnmyspawnPoints[] seria mejor pero necesitariamos prefabs de enemyspawnpoints. de todas formas tenemos 1 solo enemigo de momento
    public Transform[] spawnPoints;

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
