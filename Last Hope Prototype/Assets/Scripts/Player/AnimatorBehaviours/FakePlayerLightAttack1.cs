using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerLightAttack1 : PlayerBaseAttackState
{
    //PlayerController playerController;
    //private float h, v = 0;
    //private System.String nextAttack = null;
    //bool changedState = false;

    protected override void LoadStateSettings()
    {
        //availableAttacks = new HashSet<string>();
        availableAttacks.Add("lightAttack");
        availableAttacks.Add("heavyAttack");
        availableAttacks.Add("blueSpecialAttack");
        availableAttacks.Add("redSpecialAttack");

        attackName = "L1";
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if (playerController == null)
    //    {
    //        playerController = animator.transform.gameObject.GetComponent<PlayerController>();
    //    }
    //    playerController.ChangeAttack("L1");
    //    playerController.DisableSwordEmitter();
    //    playerController.DisableShieldEmitter();
    //    playerController.CloseInputWindow();
    //    playerController.EndSwordAttack();
    //    playerController.EndShieldAttack();

    //    h = InputManager.LeftJoystick().x;
    //    v = InputManager.LeftJoystick().z;
    //    playerController.PendingMovement(h, v);

    //    nextAttack = null;
    //    changedState = false;

    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //       if(changedState == false)
    //       {
    //           if (playerController.GetCanChangeAttackState())
    //           {
    //               //This if must be independent from upper one
    //               if(nextAttack != null)
    //               {
    //                   changedState = true;
    //                   animator.SetTrigger(nextAttack);
    //               }

    //           }
    //           else
    //           {
    //               if (InputManager.LightAttack())
    //               {
    //                   nextAttack = "lightAttack";
    //               }
    //               else if (InputManager.HeavyAttack())
    //               {
    //                   nextAttack = "heavyAttack";
    //               }
    //               else if (InputManager.SpecialAttack())
    //               {
    //                   switch (playerController.SpecialAttackToPerform())
    //                   {
    //                       case PlayerStanceType.STANCE_BLUE:
    //                           nextAttack = "blueSpecialAttack";
    //                           break;
    //                       case PlayerStanceType.STANCE_RED:
    //                           nextAttack = "redSpecialAttack";
    //                           break;
    //                   }
    //               }
    //           }

    //       }
    //   }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //   {
    //       playerController.CloseInputWindow();
    //       playerController.EndSwordAttack();
    //       nextAttack = null;
    //   }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
