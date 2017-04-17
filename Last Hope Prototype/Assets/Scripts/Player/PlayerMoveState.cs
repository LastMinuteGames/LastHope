using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerMoveState : PlayerFSM
{
    public PlayerMoveState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_MOVE)
    {

    }


}

