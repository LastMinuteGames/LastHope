using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerDodgeState : PlayerFSM
{
    private int duration = 20;
    private int startFrame;

    public PlayerDodgeState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_DODGE)
    {

    }

    public override void Start()
    {
        startFrame = Time.frameCount;
    }

    public override PlayerStateType Update()
    {

        // TODO: Call to playerController to execute the Dodge move

        if (Time.frameCount >= startFrame + duration)
        {
            return PlayerStateType.PLAYER_STATE_IDLE;
        }

        return PlayerStateType.PLAYER_STATE_DODGE;
    }
}