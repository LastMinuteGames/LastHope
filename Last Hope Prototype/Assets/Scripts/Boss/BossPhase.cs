using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Boss/Phase")]
public class BossPhase : ScriptableObject
{
	public BossEvent[] eventSequence;
	public BossEvent[] eventsLoop;
    public BossEvent currentEvent;
    private int currentEventId;

	private bool sequenceEnded;

    public virtual void StartPhase()
    {
        Debug.Log("starting a boss phase");

		sequenceEnded = false;

		if (eventSequence.Length > 0)
        {
            currentEventId = 0;
			currentEvent = eventSequence[currentEventId];
            currentEvent.StartEvent();
        }
    }

    public virtual void UpdatePhase()
    {
        bool ret = currentEvent.UpdateEvent();

		if (ret) {
			return;
		}

		if (!sequenceEnded) 
		{
			currentEvent.TerminateEvent();
			currentEventId = (currentEventId + 1);

			if (currentEventId < eventSequence.Length)
			{
				currentEvent = eventSequence[currentEventId];
				currentEvent.StartEvent ();
			} 
			else 
			{
				currentEventId = 0;
				sequenceEnded = true;
				currentEvent = eventsLoop [currentEventId];
				currentEvent.StartEvent ();
			}
		} 
		else
		{
			currentEvent.TerminateEvent();
			currentEventId = (currentEventId + 1) % eventsLoop.Length;
			currentEvent = eventsLoop[currentEventId];
			currentEvent.StartEvent();
		}
    }


    public virtual void TerminatePhase()
    {
        currentEvent.TerminateEvent();
        currentEventId = 0;
        Debug.Log("terminating phase");
    }
}
