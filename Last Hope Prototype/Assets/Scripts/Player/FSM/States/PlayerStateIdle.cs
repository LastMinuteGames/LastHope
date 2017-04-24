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

    public override void Start()
    {
        playerController.anim.SetBool("iddle", true);
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
        else if (InputManager.Interact() && playerController.canInteract)
        {
            return PlayerStateType.PLAYER_STATE_INTERACT;
        }
        else if (InputManager.Stance1())
        {
            if (playerController.IsGreyAbilityEnabled())
            {
                playerController.newStance = PlayerStance.STANCE_GREY;
                if (playerController.newStance != playerController.stance)
                {
                    return PlayerStateType.PLAYER_STATE_CHANGE_STANCE;
                }
            }
        }
        else if (InputManager.Stance2())
        {
            if (playerController.IsRedAbilityEnabled())
            {
                playerController.newStance = PlayerStance.STANCE_RED;
                if (playerController.newStance != playerController.stance)
                {
                    return PlayerStateType.PLAYER_STATE_CHANGE_STANCE;
                }
            }
        }
        else if (InputManager.Dodge())
        {
            return PlayerStateType.PLAYER_STATE_DODGE;
        }
        else if (InputManager.LightAttack())
        {
            return PlayerStateType.PLAYER_STATE_ATTACK;
        }
        else if (InputManager.SpecialAttack())
        {
            return PlayerStateType.PLAYER_STATE_SPECIAL_ATTACK;
        }

        // TODO: LIGHT, HEAVY AND SPECIAL ATTACK FOR THE COMBO SYSTEM

        return PlayerStateType.PLAYER_STATE_IDLE;
    }

    public override void End()
    {
        playerController.anim.SetBool("iddle", false);
    }
}