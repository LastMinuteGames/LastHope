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
        double diff = Time.time - msStartTime;
        if (diff >= trashState.timeToAfterDeadMS && !trashState.anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            //Debug.Log("DENTRO del if. Diff = " + diff);
            //Debug.Log("timeToAfetDead: " + trashState.timeToAfterDeadMS);
            //EndState();
            trashState.Dead();

        }
        //else
        //{
        //    Debug.Log("FUERA del if. Diff = " + diff);
        //}
        return type;
    }

    public override void StartState()
    {
        msStartTime = Time.time;
        /**
         *  TODO: Mark enemy to know that he's dead
         * */
        trashState.anim.SetBool("die", true);
        go.GetComponent<Renderer>().material.color = Color.yellow;
        Debug.Log("START TrashDeadState");
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