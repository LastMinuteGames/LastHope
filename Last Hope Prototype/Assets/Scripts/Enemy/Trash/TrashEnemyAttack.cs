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
        trashState.nav.Stop();
        trashState.anim.SetBool("attack", true);
        numberOfFrames = 0;
        Debug.Log("START TrashEnemyAttack");

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
        //if (trashState.lastAttackTime == 0 || diff >= trashState.timeAttackRefresh)
        if((!trashState.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || trashState.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) && !trashState.anim.IsInTransition(0)) // > 1 && !trashState.anim.IsInTransition(0))
        {
            trashState.lastAttackTime = msStartTime;
            //trashState.Attack();
            return TrashStateTypes.COMBAT_MOVE_BACK_STATE;
            //return TrashStateTypes.COMBAT_STATE;//new TrashChaseState(go);
        }

        return type;
    }

    public override void EndState()
    {
        trashState.anim.SetBool("attack", false);
        trashState.attackZone.SetActive(false);
    }
}

