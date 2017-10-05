using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EnemySpawnSystem;

public class NormalCombatEvent : GenericCombatEvent
{
    [SerializeField]
    public WaveBlueprint waveBP;

    override protected void InitData()
    {
        base.InitData();

        Wave wave = new Wave("1");
        wave.AddEnemy(EnemyType.ET_TRASH, waveBP.maxSpawnedEnemies, waveBP.totalEnemies);

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
    

    protected override void FinishedEvent()
    {
        base.FinishedEvent();
    }
}
