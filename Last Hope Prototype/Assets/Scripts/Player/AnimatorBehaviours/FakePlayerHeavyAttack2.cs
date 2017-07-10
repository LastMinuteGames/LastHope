using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerHeavyAttack2 : PlayerBaseAttackState
{
    protected override void LoadStateSettings()
    {
        availableAttacks.Add("blueSpecialAttack");
        availableAttacks.Add("redSpecialAttack");
        attackName = "H2";
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