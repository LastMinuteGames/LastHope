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

    public PlayerStateType Update()
    {
        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;

        return PlayerStateType.PLAYER_STATE_MOVE;
    }

    void FixedUpdate()
    {
        playerController.Move(h, v);
    }


}

