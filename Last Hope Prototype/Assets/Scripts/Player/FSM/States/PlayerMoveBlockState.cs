using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerMoveBlockState : PlayerFSM
{
    private float h, v;

    public PlayerMoveBlockState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_MOVE_BLOCKING)
    {

    }
    public override void Start()
    {
        h = 0;
        v = 0;
    }

    public override PlayerStateType Update()
    {
        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;

        if (!InputManager.Block())
        {
            return PlayerStateType.PLAYER_STATE_MOVE;
        }
        else if (InputManager.Interact() && playerController.canInteract)
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
        else if (h == 0 && v == 0)
        {
            return PlayerStateType.PLAYER_STATE_BLOCK;
        }

        // TODO: LIGHT, HEAVY AND SPECIAL ATTACK FOR THE COMBO SYSTEM

        playerController.PendingMovement(h, v);

        return PlayerStateType.PLAYER_STATE_MOVE;
    }
}

