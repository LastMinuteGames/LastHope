using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class TrashEnemyAttack : TrashState
{
    double msStartTime;

    public TrashEnemyAttack(GameObject go) : base(go, "TrashEnemyAttack")
    {
    }

    public override void StartState()
    {
        msStartTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        trashState.Attack();
    }

    public override String UpdateState()
    {
        double diff = (DateTime.Now - DateTime.MinValue).TotalMilliseconds - msStartTime;
        if (diff >= trashState.timeAttackRefresh)
        {
            return GetState(TrashStateTypes.TRASH_CHASE_STATE);//new TrashChaseState(go);
        }

        return name;
    }
}

