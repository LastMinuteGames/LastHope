using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/RestingEvent")]
public class RestingEvent : BossEvent
{

    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting RestingEvent");
        BossManager.instance.SetEmisiveIddle();
    }


    public override bool UpdateEvent()
    {
        //Add behaviour
        //Debug.Log("update RestingEvent");
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating RestingEvent");
    }

}
