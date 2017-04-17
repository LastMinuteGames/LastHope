using System;
using UnityEngine;

public class TrashDeadState : TrashState
{
    public long msToDissappear;
    private double msStartTime;
    public TrashDeadState(GameObject go) : base(go)
    {
    }

    public override IEnemyState UpdateState()
    {
        double diff = (DateTime.Now - DateTime.MinValue).TotalMilliseconds - msStartTime;
        if (diff >= trashState.timeToAfterDeadMS)
        {
            EndState();
            trashState.Dead();
            
        }
        return null;
    }

    public override void StartState()
    {
        msStartTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        /**
         *  TODO: Mark enemy to know that he's dead
         * */
        trashState.anim.SetBool("die",true);
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