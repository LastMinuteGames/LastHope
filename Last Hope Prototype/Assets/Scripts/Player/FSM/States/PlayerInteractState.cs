using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class PlayerInteractState : PlayerFSM
{
    private int duration = 20;
    private int startFrame;

    public PlayerInteractState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_INTERACT)
    {

    }
    
    public override void Start()
    {
        startFrame = Time.frameCount;
    }

    public override PlayerStateType Update()
    {
        // TODO: Call to playerController to execute the Interact move

        if (Time.frameCount >= startFrame + duration)
        {
            return PlayerStateType.PLAYER_STATE_IDLE;
        }

        return PlayerStateType.PLAYER_STATE_INTERACT;
    }
}