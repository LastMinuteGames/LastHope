//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerDeadState : PlayerFSM
//{
//    private int duration = 120;
//    private int startFrame;

//    public PlayerDeadState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_DEAD)
//    {

//    }

//    public override void Start()
//    {
//        startFrame = Time.frameCount;
//        playerController.anim.SetBool("die", true);
//    }

//    public override PlayerStateType Update()
//    {
//        if (Time.frameCount >= startFrame + duration)
//        {
//            return PlayerStateType.PLAYER_STATE_RESPAWN;
//        }
//        return PlayerStateType.PLAYER_STATE_DEAD;
//    }

//    public override void End()
//    {
//        playerController.anim.SetBool("die", false);
//    }
//}