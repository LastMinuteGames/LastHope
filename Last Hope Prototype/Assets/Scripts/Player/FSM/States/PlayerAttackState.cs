using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerAttackState : PlayerFSM
{
    public PlayerAttackState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_ATTACK)
    {

    }
}

