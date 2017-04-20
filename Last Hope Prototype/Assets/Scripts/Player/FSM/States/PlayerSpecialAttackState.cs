using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerSpecialAttackState : PlayerFSM
{
    private int duration = 2;
    private int startFrame;

    public PlayerSpecialAttackState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_SPECIAL_ATTACK)
    {

    }

    public override void Start()
    {
        startFrame = Time.frameCount;
        playerController.StartSpecialAttack();
    }

    public override PlayerStateType Update()
    {

        if (Time.frameCount >= startFrame + duration)
        {
            return PlayerStateType.PLAYER_STATE_IDLE;
        }

        playerController.SpecialAttack();
        return PlayerStateType.PLAYER_STATE_SPECIAL_ATTACK;
    }

    public override void End()
    {
        playerController.EndSpecialAttack();
    }
}

