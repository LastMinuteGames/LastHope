using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/RocketRainEvent")]
public class RocketRainEvent : BossEvent
{
    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting RocketRainEvent");
    }

    public override bool UpdateEvent()
    {
        Debug.Log("update RocketRainEvent");
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating RocketRainEvent");
    }
}
