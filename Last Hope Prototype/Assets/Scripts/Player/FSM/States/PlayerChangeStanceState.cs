using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerChangeStanceState : PlayerFSM
{
    public PlayerChangeStanceState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_CHANGE_STANCE)
    {

    }

    public override PlayerStateType Update()
    {
        playerController.ChangeStance(newStance);

        // if CHANGE STANCE finished return IDLE

        return PlayerStateType.PLAYER_STATE_CHANGE_STANCE;
    }
}