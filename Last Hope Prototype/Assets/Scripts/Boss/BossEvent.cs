using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BossEvent : ScriptableObject
{
    public float duration = 2;
	public float anticipationTime = 1;
    public float ellapsedTime = 0;

    public virtual void StartEvent()
    {
        //Debug.Log("start event");
        ellapsedTime = 0;
    }

    public virtual bool UpdateEvent()
    {
        //Debug.Log("update event");
        ellapsedTime += Time.deltaTime;
        return ellapsedTime < duration;
    }

    public virtual void TerminateEvent()
    {
        //Debug.Log("terminate event");
        ellapsedTime = 0;
    }
}
