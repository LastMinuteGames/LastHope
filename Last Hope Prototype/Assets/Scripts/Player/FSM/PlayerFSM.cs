using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerFSM : PlayerState, IPlayerFSM
{
    protected GameObject go;
    protected PlayerController playerController;

   public PlayerFSM(GameObject go, PlayerStateType type) : base(type)
    {
        this.go = go;
        this.playerController = go.GetComponent<PlayerController>();
    }

    virtual public void ChangeState(PlayerStateType type)
    {
        //TODO: END current state on player. Change currentState and start CurrentState
    }

    virtual public void End()
    {
        
    }

    virtual public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            ChangeState(PlayerStateType.PLAYER_STATE_DAMAGED);
        }
    }

    virtual public void OnTriggerExit(Collider other)
    {
        
    }

    virtual public void Start()
    {
        numberOfFrames = 0;
    }

    virtual public PlayerStateType Update()
    {
        return type;
    }


    public PlayerStateType Type()
    {
        return type;
    }
}

