//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using UnityEngine;

//public class PlayerAttackState : PlayerFSM
//{
//    private int duration = 50;
//    private int startFrame;

//    public PlayerAttackState(GameObject go) : base(go, PlayerStateType.PLAYER_STATE_ATTACK)
//    {

//    }

//    public override void Start()
//    {
//        startFrame = Time.frameCount;
//        playerController.StartAttack();
//        playerController.anim.SetBool("attack", true);
//    }

//    public override PlayerStateType Update()
//    {
//        if (Time.frameCount >= startFrame + duration)
//        {
//            return PlayerStateType.PLAYER_STATE_IDLE;
//        }

//        return PlayerStateType.PLAYER_STATE_ATTACK;
//    }

//    public override void End()
//    {
//        playerController.EndAttack();
//        playerController.anim.SetBool("attack", false);
//    }
//}

