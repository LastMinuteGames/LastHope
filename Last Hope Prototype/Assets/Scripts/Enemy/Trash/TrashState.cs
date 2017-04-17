using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;





public class TrashState : EnemyState, IEnemyState
{

    protected GameObject go;
    protected EnemyTrash trashState;

    public TrashState(GameObject go, TrashStateTypes type) : base(type)
    {
        this.go = go;
        trashState = go.GetComponent<EnemyTrash>();
    }

    public virtual TrashStateTypes UpdateState()
    {
        if (trashState.target != null)
        {
            trashState.nav.SetDestination(trashState.target.position);
        }
        return type;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            trashState.ChangeState(TrashStateTypes.CHASE_STATE);

            /**
             *  TODO: Get damage from player!
            **/
            int damage = 10;
            trashState.TakeDamage(damage);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trashState.target = other.transform;
            trashState.ChangeState(TrashStateTypes.CHASE_STATE);
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            trashState.currentState.EndState();

            if (trashState.life <= 0)
                trashState.ChangeState(TrashStateTypes.DEAD_STATE);
            else
                trashState.ChangeToPreviousState();

            trashState.currentState.StartState();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trashState.ChangeToPreviousState();
        }
    }

    public void OnPlayerInRange(Collider other)
    {
        //if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    trashState.currentState.EndState();
        //    trashState.currentState = new TrashEnemyAttack(go);
        //    trashState.currentState.StartState();
        //}
    }

    public TrashStateTypes Type()
    {
        return type;
    }
}
