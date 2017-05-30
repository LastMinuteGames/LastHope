using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerLightAttack2 : StateMachineBehaviour
{
    PlayerController playerController;
    private float h, v = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
        {
            playerController = animator.transform.gameObject.GetComponent<PlayerController>();
        }
        playerController.ChangeAttack("L2");
        playerController.CloseInputWindow();
        playerController.EndAttack();

        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;
        playerController.PendingMovement(h, v);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController.GetInputWindowState())
        {
            if (InputManager.LightAttack())
            {
                animator.SetTrigger("lightAttack");
            }
            else if (InputManager.HeavyAttack())
            {
                animator.SetTrigger("heavyAttack");
            }
            else if (InputManager.SpecialAttack())
            {
                switch (playerController.SpecialAttackToPerform())
                {
                    case PlayerStance.STANCE_BLUE:
                        animator.SetTrigger("blueSpecialAttack");
                        break;
                    case PlayerStance.STANCE_RED:
                        animator.SetTrigger("redSpecialAttack");
                        break;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.CloseInputWindow();
        playerController.EndAttack();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
