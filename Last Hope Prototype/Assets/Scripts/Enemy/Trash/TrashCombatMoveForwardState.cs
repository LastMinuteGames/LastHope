using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TrashCombatMoveForwardState : TrashState
{
    public TrashCombatMoveForwardState(GameObject go) : base(go, TrashStateTypes.COMBAT_MOVE_FORWARD_STATE)
    {
    }

    public override void StartState()
    {
        trashState.anim.SetBool("walk", true);
        trashState.nav.Resume();
        Debug.Log("START TrashCombatMoveForwardState");
    }

    public override TrashStateTypes UpdateState()
    {
        int probability = UnityEngine.Random.Range(0, 100);
        if (trashState.attackProbability >= probability /*&& trashState.nav.remainingDistance <= trashState.attackRange*/)
        {
            trashState.nav.Stop();
            return TrashStateTypes.ATTACK_STATE;
        }
        
        trashState.nav.SetDestination(trashState.target.position);

        return type;
    }

    public override void EndState()
    {
        trashState.anim.SetBool("walk", false);
    }
}
