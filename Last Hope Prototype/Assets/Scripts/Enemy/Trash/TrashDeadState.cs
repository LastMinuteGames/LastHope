using System;
using UnityEngine;

public class TrashDeadState : TrashState
{
    public long msToDissappear;
    private double msStartTime;
    public TrashDeadState(GameObject go) : base(go, TrashStateTypes.DEAD_STATE)
    {
    }

    public override TrashStateTypes UpdateState()
    {
        double diff = (DateTime.Now - DateTime.MinValue).TotalMilliseconds - msStartTime;
        if (diff >= trashState.timeToAfterDeadMS)
        {
            EndState();
            trashState.Dead();

        }
        return type;
    }

    public override void StartState()
    {
        msStartTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        /**
         *  TODO: Mark enemy to know that he's dead
         * */
        go.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public override void EndState()
    {
    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    public override void OnTriggerExit(Collider other)
    {
    }
}