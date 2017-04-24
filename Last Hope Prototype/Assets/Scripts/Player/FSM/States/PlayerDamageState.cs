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

    public override void Start()
    {
        playerController.anim.SetBool("hit1", true);
    }

    public override void End()
    {
        playerController.anim.SetBool("hit1", false);
    }
}