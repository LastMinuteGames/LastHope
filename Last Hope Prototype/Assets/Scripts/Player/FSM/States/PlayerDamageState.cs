using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerDamageState : PlayerFSM
{
    private int duration = 45;
    private int startFrame;

    public PlayerDamageState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_DAMAGED)
    {

    }

    public override void Start()
    {
        startFrame = Time.frameCount;
        playerController.anim.SetBool("hit1", true);
    }

    public override PlayerStateType Update()
    {
        if (Time.frameCount >= startFrame + duration)
        {
            return PlayerStateType.PLAYER_STATE_IDLE;
        }

        return PlayerStateType.PLAYER_STATE_DAMAGED;
    }

    public override void End()
    {
        playerController.anim.SetBool("hit1", false);
    }
}