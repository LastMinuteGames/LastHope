using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerChangeStanceState : PlayerFSM
{
    private int duration = 30;
    private int startFrame;

    public PlayerChangeStanceState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_CHANGE_STANCE)
    {

    }

    public override void Start()
    {
        startFrame = Time.frameCount;
        playerController.ChangeStance(playerController.newStance);
    }

    public override PlayerStateType Update()
    {
        if (Time.frameCount >= startFrame + duration)
        {
            return PlayerStateType.PLAYER_STATE_IDLE;
        }

        return PlayerStateType.PLAYER_STATE_CHANGE_STANCE;
    }
}