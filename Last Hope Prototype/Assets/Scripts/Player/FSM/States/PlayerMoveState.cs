using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerMoveState : PlayerFSM
{
    private float h, v;

    public PlayerMoveState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_MOVE)
    {

    }

    public override PlayerStateType Update()
    {
        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;

        if (InputManager.Block())
        {
            return PlayerStateType.PLAYER_STATE_MOVE_BLOCKING;
        }
        else if (h == 0 && v == 0)
        {
            return PlayerStateType.PLAYER_STATE_IDLE;
        }
        return PlayerStateType.PLAYER_STATE_MOVE;
    }

    void FixedUpdate()
    {
        playerController.Move(h, v);
    }

    public override void End()
    {

    }
}

