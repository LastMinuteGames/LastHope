using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashState : IEnemyState
{

    protected GameObject go;

    public TrashState(GameObject go)
    {
        this.go = go;
    }

    public virtual IEnemyState UpdateState()
    {
        EnemyTrash enemyTrash = go.GetComponent<EnemyTrash>();
        if (enemyTrash.target != null)
        {
            enemyTrash.nav.SetDestination(enemyTrash.target.position);
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
            EnemyTrash trashState = go.GetComponent<EnemyTrash>();
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
            EnemyTrash trashState = go.GetComponent<EnemyTrash>();
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
            EnemyTrash trashState = go.GetComponent<EnemyTrash>();
            trashState.currentState.EndState();

            if (trashState.life <= 0)
                trashState.currentState = new TrashDeadState(go);
            else
                trashState.currentState = new TrashIdleState(go);

            trashState.currentState.StartState();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EnemyTrash trashState = go.GetComponent<EnemyTrash>();
            trashState.currentState.EndState();
            trashState.currentState = new TrashIdleState(go);
            trashState.currentState.StartState();
        }
    }

    public void OnPlayerInRange(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EnemyTrash trashState = go.GetComponent<EnemyTrash>();
            trashState.currentState.EndState();
            trashState.currentState = new TrashEnemyAttack(go);
            trashState.currentState.StartState();
        }
    }
}
