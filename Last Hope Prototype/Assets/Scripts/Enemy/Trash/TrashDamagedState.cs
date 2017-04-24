using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TrashDamagedState : TrashState
{
    public TrashDamagedState(GameObject go) : base(go, TrashStateTypes.DAMAGED_STATE)
    {
    }

    public override void StartState()
    {
        //EnemyTrash trashState = go.GetComponent<EnemyTrash>();
        trashState.anim.SetBool("hit1", true);
        Debug.Log("START TrashDamagedState");
    }

    public override void EndState()
    {
        //Exist from state
        trashState.anim.SetBool("hit1", false);
    }

}
