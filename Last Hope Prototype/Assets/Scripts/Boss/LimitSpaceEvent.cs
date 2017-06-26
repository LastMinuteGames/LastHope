using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/LimitSpaceEvent")]
public class LimitSpaceEvent : BossEvent
{

    public override void StartEvent()
    {
        base.StartEvent();
        //Debug.Log("starting LimitSpaceEvent");
    }

    public override bool UpdateEvent()
    {
        //Debug.Log("update LimitSpaceEvent");
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating LimitSpaceEvent");
    }
}
