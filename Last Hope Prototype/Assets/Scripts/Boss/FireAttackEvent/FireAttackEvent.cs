using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/FireAttackEvent")]
public class FireAttackEvent : BossEvent
{

    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting FireAttackEvent");
    }

    public override bool UpdateEvent()
    {
        //Add behaviour
        Debug.Log("update FireAttackEvent");
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating FireAttackEvent");
    }
}
