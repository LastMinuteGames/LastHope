using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TrashCombatMoveBackState : TrashState
{
    private float startTime = 0;
    private float speed = 50;

    public TrashCombatMoveBackState(GameObject go) : base(go, TrashStateTypes.COMBAT_MOVE_BACK_STATE)
    {
    }

    public override void StartState()
    {
        startTime = Time.time;
        trashState.nav.Stop();
    }

    public override TrashStateTypes UpdateState()
    {
        if(trashState.nav.remainingDistance >= trashState.attackRange && trashState.nav.remainingDistance <= trashState.combatRange)
        {
            return TrashStateTypes.COMBAT_STATE;
        }

        trashState.transform.Translate(Vector3.back * speed * Time.deltaTime);
        return type;
    }
}

