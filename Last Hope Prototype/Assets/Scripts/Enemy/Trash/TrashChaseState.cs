using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TrashChaseState : TrashState
{
    public TrashChaseState(GameObject go) : base(go, "TrashChaseState")
    {
    }

    public override void StartState()
    {
        //EnemyTrash trashState = go.GetComponent<EnemyTrash>();
    }

    public override String UpdateState()
    {
        if (trashState.target != null)
        {
            if (trashState.nav.remainingDistance >= trashState.combatRange)
            {
                trashState.nav.SetDestination(trashState.target.position);
            }
            else
            {
                trashState.nav.Stop();
            }
        }
        return name;
    }
}
