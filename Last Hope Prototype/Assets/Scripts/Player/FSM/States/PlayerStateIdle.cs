using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class PlayerIdleState : PlayerFSM
{
    public PlayerIdleState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_IDLE)
    {
        
    }

    public override PlayerStateType Update()
    {
        if (InputManager.LeftJoystick().Equals(Vector3.zero) == false)
        {
            return PlayerStateType.PLAYER_STATE_MOVE;
        }
        else if (InputManager.Block())
        {
            return PlayerStateType.PLAYER_STATE_BLOCK;
        }
        else if (InputManager.Interact())
        {
            return PlayerStateType.PLAYER_STATE_INTERACT;
        }
        else if (InputManager.Stance1())
        {
            newStance = PlayerStance.STANCE_GREY;
            return PlayerStateType.PLAYER_STATE_CHANGE_STANCE;
        }
        else if (InputManager.Stance2())
        {
            newStance = PlayerStance.STANCE_RED;
            return PlayerStateType.PLAYER_STATE_CHANGE_STANCE;
        }
        else if (InputManager.Dodge())
        {
            return PlayerStateType.PLAYER_STATE_DODGE;
        }

        // TODO: LIGHT, HEAVY AND SPECIAL ATTACK FOR THE COMBO SYSTEM

        return PlayerStateType.PLAYER_STATE_UNDEFINED;
    }
}