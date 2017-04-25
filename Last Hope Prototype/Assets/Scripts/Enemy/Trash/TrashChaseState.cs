using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TrashChaseState : TrashState
{
    public TrashChaseState(GameObject go) : base(go, TrashStateTypes.CHASE_STATE)
    {
        

    }

    public override void StartState()
    {
        numberOfFrames = 0;
        //EnemyTrash trashState = go.GetComponent<EnemyTrash>();
        trashState.anim.SetBool("walk", true);
        Debug.Log("START TrashChaseState");
    }

    public override void EndState()
    {
        //Exist from state
        trashState.anim.SetBool("walk", false);
    }

    public override TrashStateTypes UpdateState()
    {
        //IS THIS IF NECCESSARY?
        if(numberOfFrames != 0 && numberOfFrames % trashState.frameUpdateInterval == 0)
        {
            ++numberOfFrames;
            return type;
        }


        if (trashState.target != null)
        {
            if (trashState.nav.remainingDistance >= trashState.combatRange)
            {
                trashState.nav.SetDestination(trashState.target.position);
                trashState.nav.Resume();
            }
            else
            {
                trashState.nav.Stop();
                return TrashStateTypes.COMBAT_STATE;
            }
        }
        ++numberOfFrames;
        return type;
    }
}
