using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerDamageState : PlayerFSM
{
    public PlayerDamageState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_DAMAGED)
    {

    }
}