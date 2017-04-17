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
    }

    public override TrashStateTypes UpdateState()
    {
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
