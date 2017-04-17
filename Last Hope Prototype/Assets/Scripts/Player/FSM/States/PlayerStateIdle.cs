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
}
