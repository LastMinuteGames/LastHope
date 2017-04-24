using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TrashCombatState : TrashState
{
    private int attackProbability = 0;
    private int approachProbability = 0;

    public TrashCombatState(GameObject go) : base(go, TrashStateTypes.COMBAT_STATE)
    {
        //probabilities.Add(trashState.attackProbability, TrashStateTypes.ATTACK_STATE);
        //probabilities.Add(trashState.approachProbability, TrashStateTypes.COMBAT_STATE);

        //probabilities.Add(trashState.moveAroundPlayerProbability, TrashStateTypes.ATTACK_STATE);
    }

    public override void StartState()
    {
        numberOfFrames = 0;
        attackProbability = trashState.attackProbability;
        approachProbability = trashState.approachProbability;
        Debug.Log("START TrashCombatState");

    }

    public override TrashStateTypes UpdateState()
    {
        if (numberOfFrames != 0 && numberOfFrames % trashState.frameUpdateInterval == 0)
        {
            ++numberOfFrames;
            return type;
        }

        if (trashState.target != null)
        {
            if(trashState.target.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (trashState.nav.remainingDistance >= trashState.combatRange)
                {
                    return TrashStateTypes.CHASE_STATE;
                }
                else
                {
                    int probability = UnityEngine.Random.Range(0, 100);

                    if (trashState.approachProbability >= probability && trashState.nav.remainingDistance >= trashState.attackRange)
                    {
                        trashState.nav.Stop();
                        return TrashStateTypes.COMBAT_MOVE_AROUND_STATE;
                    }
                    if(trashState.attackProbability >= probability && trashState.nav.remainingDistance <= trashState.attackRange)
                    {
                        trashState.nav.Stop();
                        return TrashStateTypes.ATTACK_STATE;
                    }

                    return TrashStateTypes.COMBAT_MOVE_AROUND_STATE;
                    //trashState.transform.RotateAround(trashState.target.transform.position, Vector3.up, trashState.combatAngularSpeed * Time.deltaTime);
                }
            }
        }
        ++numberOfFrames;
        return type;
    }
}

