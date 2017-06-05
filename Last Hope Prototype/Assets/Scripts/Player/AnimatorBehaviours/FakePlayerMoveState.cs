using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerMoveState : StateMachineBehaviour {
    PlayerController playerController;
    bool attacking = false;
    private float h, v = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attacking = false;
        if (playerController == null)
        {
            playerController = animator.transform.gameObject.GetComponent<PlayerController>();
        }
        //playerController.speed = playerController.normalSpeed;
        playerController.anim.SetBool("move", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;
        bool change = true;

        if (!attacking)
        {
            if (InputManager.Interact() && playerController.canInteract)
            {
                animator.SetTrigger("interact");
            }
            else if (InputManager.Stance1() && playerController.IsBlueAbilityEnabled() &&
                playerController.stance != PlayerStanceType.STANCE_BLUE)
            {
                playerController.newStance = PlayerStanceType.STANCE_BLUE;
                playerController.anim.SetTrigger("changeStance");
            }
            else if (InputManager.Stance2() && playerController.IsRedAbilityEnabled() &&
                playerController.stance != PlayerStanceType.STANCE_RED)
            {
                    playerController.newStance = PlayerStanceType.STANCE_RED;
                    playerController.anim.SetTrigger("changeStance");

            }
            else if (h == 0 && v == 0)
            {
                playerController.anim.SetBool("idle", true);
            }
            else if (InputManager.Dodge())
            {
                playerController.anim.SetTrigger("dodge");
            }
            else if (InputManager.LightAttack())
            {
                animator.SetTrigger("lightAttack");
                attacking = true;
            }
            else if (InputManager.HeavyAttack())
            {
                animator.SetTrigger("heavyAttack");
                attacking = true;
            }
            else if (InputManager.SpecialAttack())
            {
                switch (playerController.SpecialAttackToPerform())
                {
                    case PlayerStanceType.STANCE_BLUE:
                        animator.SetTrigger("blueSpecialAttack");
                        break;
                    case PlayerStanceType.STANCE_RED:
                        animator.SetTrigger("redSpecialAttack");
                        break;
                }
            }
            else
            {
                change = false;
            }

            if (change)
            {
                playerController.anim.SetBool("move", false);
            }
            else
            {
                playerController.PendingMovement(h, v);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
