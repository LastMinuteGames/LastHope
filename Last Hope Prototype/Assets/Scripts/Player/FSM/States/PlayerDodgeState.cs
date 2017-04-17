using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerDodgeState : PlayerFSM
{
    public PlayerDodgeState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_DODGE)
    {

    }
}