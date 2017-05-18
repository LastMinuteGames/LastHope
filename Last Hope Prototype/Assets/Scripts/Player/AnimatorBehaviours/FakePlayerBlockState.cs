using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerBlockState : StateMachineBehaviour
{
    PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
        {
            playerController = animator.transform.gameObject.GetComponent<PlayerController>();
        }
        playerController.anim.SetBool("block", true);
        playerController.SetBlocking(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (InputManager.LeftJoystick().Equals(Vector3.zero) == false)
        {
            playerController.anim.SetBool("move", true);
        }
        else if (!InputManager.Block())
        {
            playerController.anim.SetBool("block", false);
        }
        else if (InputManager.Interact() && playerController.canInteract)
        {
            playerController.anim.SetBool("interact", true);
        }
        else if (InputManager.Stance1())
        {
            if (playerController.IsGreyAbilityEnabled())
            {
                playerController.newStance = PlayerStance.STANCE_GREY;
                if (playerController.newStance != playerController.stance)
                {
                    playerController.anim.SetBool("changeStance", true);
                }
            }
        }
        else if (InputManager.Stance2())
        {
            if (playerController.IsRedAbilityEnabled())
            {
                playerController.newStance = PlayerStance.STANCE_RED;
                if (playerController.newStance != playerController.stance)
                {
                    playerController.anim.SetBool("changeStance", true);
                }
            }
        }
        else if (InputManager.Dodge())
        {
            playerController.anim.SetBool("dodge", true);
        }
        else if (InputManager.LightAttack())
        {
            playerController.anim.SetBool("attack", true);
        }
        else if (InputManager.SpecialAttack())
        {
            playerController.anim.SetBool("specialAttack", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.anim.SetBool("block", false);
        playerController.SetBlocking(false);
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
