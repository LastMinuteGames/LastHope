using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerMoveBlockState : PlayerFSM
{
    public PlayerMoveBlockState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_MOVE_BLOCKING)
    {

    }
}

