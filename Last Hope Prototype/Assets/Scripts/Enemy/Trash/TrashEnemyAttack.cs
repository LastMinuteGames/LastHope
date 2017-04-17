using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class TrashEnemyAttack : TrashState
{
    double msStartTime;

    public TrashEnemyAttack(GameObject go) : base(go)
    {
    }

    public override void StartState()
    {
        msStartTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        trashState.Attack();
        trashState.anim.SetBool("attack", true);
    }

    public override void EndState()
    {
        trashState.anim.SetBool("attack",false);
    }

    public override IEnemyState UpdateState()
    {
        double diff = (DateTime.Now - DateTime.MinValue).TotalMilliseconds - msStartTime;
        if (diff >= trashState.timeAttackRefresh)
        {
            return new TrashChaseState(go);
        }

        return null;
    }
}

