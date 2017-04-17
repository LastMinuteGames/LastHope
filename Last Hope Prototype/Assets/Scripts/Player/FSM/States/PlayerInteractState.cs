using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerInteractState : PlayerFSM
{
    public PlayerInteractState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_INTERACT)
    {

    }
}