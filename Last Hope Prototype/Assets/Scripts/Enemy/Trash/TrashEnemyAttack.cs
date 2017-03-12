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
        EnemyTrash trashState = go.GetComponent<EnemyTrash>();
        trashState.Attack();
    }

    public override IEnemyState UpdateState()
    {
        EnemyTrash trashState = go.GetComponent<EnemyTrash>();
        double diff = (DateTime.Now - DateTime.MinValue).TotalMilliseconds - msStartTime;
        if (diff >= trashState.timeAttackRefresh)
        {
            return new TrashIdleState(go);
        }

        return null;
    }
}

