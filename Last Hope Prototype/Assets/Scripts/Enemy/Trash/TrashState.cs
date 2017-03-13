using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashState : IEnemyState
{

    protected GameObject go;
    protected EnemyTrash trashState;

    public TrashState(GameObject go)
    {
        this.go = go;
        trashState = go.GetComponent<EnemyTrash>();
    }

    public virtual IEnemyState UpdateState()
    {
        if (trashState.target != null)
        {
            trashState.nav.SetDestination(trashState.target.position);
        }
        return null;
    }

    public virtual void StartState()
    {
        //Init state
    }

    public virtual void EndState()
    {
        //Exist from state
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            trashState.currentState.EndState();
            trashState.currentState = new TrashDamagedState(go);
            trashState.currentState.StartState();
            /**
             *  TODO: Get damage from player!
            **/
            int damage = 10;
            trashState.TakeDamage(damage);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trashState.currentState.EndState();
            trashState.target = other.transform;
            trashState.currentState = new TrashChaseState(go);
            trashState.currentState.StartState();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            trashState.currentState.EndState();

            if (trashState.life <= 0)
                trashState.currentState = new TrashDeadState(go);
            else
                trashState.ChangeToPreviousState();

            trashState.currentState.StartState();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trashState.currentState.EndState();
            trashState.ChangeToPreviousState();
            trashState.currentState.StartState();
        }
    }

    public void OnPlayerInRange(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trashState.currentState.EndState();
            trashState.currentState = new TrashEnemyAttack(go);
            trashState.currentState.StartState();
        }
    }
}
