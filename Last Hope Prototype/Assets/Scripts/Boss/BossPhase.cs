using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Boss/Phase")]
public class BossPhase : ScriptableObject
{
    public BossEvent[] bossEvents;
    private int currentEventId;
    public BossEvent currentEvent;


    public virtual void StartPhase()
    {
        Debug.Log("starting a boss phase");
        if (bossEvents.Length > 0)
        {
            currentEventId = 0;
            currentEvent = bossEvents[currentEventId];
            currentEvent.StartEvent();
        }
    }

    public virtual void UpdatePhase()
    {
        //Debug.Log("phase update");
        bool ret = currentEvent.UpdateEvent();
        Debug.Log(ret);
        if (!ret)
        {
            currentEventId = (currentEventId + 1) % bossEvents.Length;
            currentEvent = bossEvents[currentEventId];
            currentEvent.StartEvent();
        }
    }


    public virtual void TerminatePhase()
    {
        Debug.Log("terminating phase");

        currentEvent.TerminateEvent();
        currentEventId = 0;
    }
}
