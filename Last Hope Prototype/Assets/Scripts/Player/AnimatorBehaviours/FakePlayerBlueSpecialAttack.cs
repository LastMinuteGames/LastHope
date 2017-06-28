using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerBlueSpecialAttack : PlayerBaseAttackState
{
    protected override void LoadStateSettings()
    {
        attackName = "Blue";
    }
    //PlayerController playerController;

    //// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Player_Combat_SpecialAttackBlue);
    }
        //    if (playerController == null)
        //    {
        //        playerController = animator.transform.gameObject.GetComponent<PlayerController>();
        //    }
        //    playerController.ChangeAttack("Blue");
        //    playerController.CloseInputWindow();
        //}

        //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{

        //}

        //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    playerController.EndBlueSpecialAttack();
        //}

        //// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
        ////override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ////
        ////}

        //// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
        ////override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ////
        ////}
    }
