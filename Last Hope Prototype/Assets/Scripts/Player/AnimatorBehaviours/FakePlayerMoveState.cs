using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerMoveState : StateMachineBehaviour {
    PlayerController playerController;
    private float h, v = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
        {
            playerController = animator.transform.gameObject.GetComponent<PlayerController>();
        }
        playerController.speed = playerController.normalSpeed;
        playerController.anim.SetBool("move", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;

        if (InputManager.Block())
        {
            playerController.anim.SetBool("block", true);
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
        else if (h == 0 && v == 0)
        {
            playerController.anim.SetBool("idle", true);
        }
        else if (InputManager.LightAttack())
        {
            playerController.anim.SetBool("attack", true);
        }
        else if (InputManager.SpecialAttack())
        {
            playerController.anim.SetBool("specialAttack", true);
        }

        // TODO: LIGHT, HEAVY AND SPECIAL ATTACK FOR THE COMBO SYSTEM

        playerController.PendingMovement(h, v);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.anim.SetBool("move", false);
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
