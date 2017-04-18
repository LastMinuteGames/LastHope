using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class TrashEnemyAttack : TrashState
{
    double msStartTime;

    public TrashEnemyAttack(GameObject go) : base(go, TrashStateTypes.ATTACK_STATE)
    {
    }

    public override void StartState()
    {
        msStartTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        //COMMENTED BECAUSE DON'T KNOW IF NECCESSARY
        //trashState.Attack();
        
        trashState.anim.SetBool("attack", true);
        numberOfFrames = 0;
    }

    public override TrashStateTypes UpdateState()
    {
        if (numberOfFrames != 0 && numberOfFrames % trashState.frameUpdateInterval == 0)
        {
            trashState.Attack();
            ++numberOfFrames;
            return type;
        }
        
        double diff = msStartTime - trashState.lastAttackTime;
        if (trashState.lastAttackTime == 0 || diff >= trashState.timeAttackRefresh)
        {
            trashState.lastAttackTime = msStartTime;
            trashState.Attack();
            return TrashStateTypes.COMBAT_STATE;//new TrashChaseState(go);
        }

        return type;
    }

    public override void EndState()
    {
        trashState.anim.SetBool("attack", false);
        trashState.attackZone.SetActive(false);
    }
}

