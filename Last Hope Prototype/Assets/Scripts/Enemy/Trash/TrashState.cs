using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum TrashStateTypes
{
    TRASH_IDLE_STATE,
    TRASH_ATTACK_STATE,
    TRASH_DEAD_STATE,
    TRASH_DAMAGED_STATE,
    TRASH_CHASE_STATE,
    TRASH_UNDEFINED_STATE
}


public class TrashState : State, IEnemyState
{

    protected GameObject go;
    protected EnemyTrash trashState;

    public TrashState(GameObject go, String name) : base(name)
    {
        this.go = go;
        trashState = go.GetComponent<EnemyTrash>();
    }

    public virtual String UpdateState()
    {
        if (trashState.target != null)
        {
            trashState.nav.SetDestination(trashState.target.position);
        }
        return name;
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
            trashState.ChangeState(GetState(TrashStateTypes.TRASH_CHASE_STATE));

            /**
             *  TODO: Get damage from player!
            **/
            int damage = 10;
            trashState.TakeDamage(damage);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trashState.target = other.transform;
            trashState.ChangeState(GetState(TrashStateTypes.TRASH_CHASE_STATE));
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            trashState.currentState.EndState();

            if (trashState.life <= 0)
                trashState.ChangeState(GetState(TrashStateTypes.TRASH_DEAD_STATE));
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            trashState.currentState.EndState();
            trashState.currentState = new TrashEnemyAttack(go);
            trashState.currentState.StartState();
        }
    }

    protected String GetState(TrashStateTypes type)
    {
        switch (type)
        {
            case TrashStateTypes.TRASH_IDLE_STATE:
                return "TrashIdleState";
            case TrashStateTypes.TRASH_DEAD_STATE:
                return "TrashDeadState";
            case TrashStateTypes.TRASH_ATTACK_STATE:
                return "TrashEnemyAttack";
            case TrashStateTypes.TRASH_CHASE_STATE:
                return "TrashChaseState";
            case TrashStateTypes.TRASH_DAMAGED_STATE:
                return "TrashDamagedState";
            default:
                return "";
        }
    }

    public String GetName()
    {
        return name;
    }
}
