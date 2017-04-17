using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TrashChaseState : TrashState
{
    public TrashChaseState(GameObject go) : base(go)
    {
    }

    public override void StartState()
    {
        //EnemyTrash trashState = go.GetComponent<EnemyTrash>();
        Debug.Log("Entro en chase!");
        trashState.anim.SetBool("walk", true);
    }

    public override void EndState()
    {
        //Exist from state
        Debug.Log("Salgo de chase!");
        trashState.anim.SetBool("walk", false);
    }

    public override IEnemyState UpdateState()
    {

        if (trashState.target != null)
        {
            trashState.nav.SetDestination(trashState.target.position);
        }
        return null;
    }

    //public override void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        EnemyTrash trashState = go.GetComponent<EnemyTrash>();
    //        trashState.currentState.EndState();
    //        trashState.currentState = new TrashIdleState(go);
    //        trashState.currentState.StartState();
    //    }
    //}
}
