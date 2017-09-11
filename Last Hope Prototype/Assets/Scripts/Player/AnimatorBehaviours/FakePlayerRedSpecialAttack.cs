using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerRedSpecialAttack : PlayerBaseAttackState
{
    protected override void LoadStateSettings()
    {
        attackName = "Red";
    }

    //// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        playerController.StartCurrentAttack();

    }

    //// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    ////override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    ////
    ////}

    //// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    ////override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    ////
    ////}
}
