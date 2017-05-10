//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//class TrashCombatMoveBackState : TrashState
//{
//    private float startTime = 0;
//    private float speed = 10;

//    public TrashCombatMoveBackState(GameObject go) : base(go, TrashStateTypes.COMBAT_MOVE_BACK_STATE)
//    {
//    }

//    public override void StartState()
//    {
//        startTime = Time.time;
//        trashState.nav.Stop();
//        trashState.anim.SetBool("walk", true);
//        Debug.Log("START TrashCombatMoveBackState");
//    }

//    public override void EndState()
//    {
//        trashState.anim.SetBool("walk", false);
//    }

//    public override TrashStateTypes UpdateState()
//    {
//        int probability = UnityEngine.Random.Range(0, 100);
//        if(trashState.attackProbability >= probability && trashState.nav.remainingDistance <= trashState.attackRange)
//        {
//            trashState.nav.Stop();
//            return TrashStateTypes.ATTACK_STATE;
//        }
//        if (Time.time - startTime > 0.3)
//        {
//            return TrashStateTypes.COMBAT_STATE;
//        }
//        //if (trashState.nav.remainingDistance >= trashState.attackRange && trashState.nav.remainingDistance <= trashState.combatRange)
//        //{
//        //    return TrashStateTypes.COMBAT_STATE;
//        //}


//        trashState.transform.Translate(Vector3.back * speed * Time.deltaTime);
//        return type;
//    }
//}

