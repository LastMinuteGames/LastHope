using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerBlockState : PlayerFSM
{
    public PlayerBlockState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_BLOCK)
    {

    }
}

