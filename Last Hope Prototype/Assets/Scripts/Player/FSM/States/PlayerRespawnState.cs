using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnState : PlayerFSM
{
    private int duration = 120;
    private int startFrame;

    public PlayerRespawnState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_RESPAWN)
    {

    }

    public override void Start()
    {
        startFrame = Time.frameCount;
        playerController.anim.SetBool("respawn", true);
    }

    public override PlayerStateType Update()
    {
        if (Time.frameCount >= startFrame + duration)
        {
            return PlayerStateType.PLAYER_STATE_IDLE;
        }
        return PlayerStateType.PLAYER_STATE_RESPAWN;
    }

    public override void End()
    {
        playerController.Respawn();
        playerController.anim.SetBool("respawn", false);
    }
}