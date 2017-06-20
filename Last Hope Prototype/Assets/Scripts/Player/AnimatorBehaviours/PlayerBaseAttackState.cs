using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerBaseAttackState : StateMachineBehaviour
{
    protected PlayerController playerController;
    protected float h, v = 0;
    protected System.String nextAttack = null;
    protected bool changedState = false;
    protected String attackName = "";
    protected Attack attack = null;
    protected HashSet<String> availableAttacks = new HashSet<string>();


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Enter in " + attackName);
        if (playerController == null)
        {
            playerController = animator.transform.gameObject.GetComponent<PlayerController>();
        }

        //Load settings for each attack state. PlayerController is available so we can change everything from here
        LoadStateSettings();

        Debug.Assert(playerController != null);
        attack = playerController.ChangeAttack(attackName);
        Debug.Assert(attack != null);
        playerController.DisableSwordEmitter();
        playerController.DisableShieldEmitter();
        //playerController.CloseInputWindow();
        playerController.EndCurrentAttack();
        //playerController.EndShieldAttack();

        h = InputManager.LeftJoystick().x;
        v = InputManager.LeftJoystick().z;
        playerController.PendingMovement(h, v);

        nextAttack = null;
        changedState = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (UpdateAttack())
        {
            //Debug.Log("Trigger Setter in " + attackName);
            animator.SetTrigger(nextAttack);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Exit in " + attackName);
        //playerController.CloseInputWindow();
        playerController.EndCurrentAttack();
        nextAttack = null;
    }

    //Returns true if we have to activate some trigger, false otherwise
    protected bool UpdateAttack()
    {
        if (changedState == false)
        {
            if (playerController.GetCanChangeAttackState())
            {
                //This if must be independent from upper one
                if (nextAttack != null)
                {
                    changedState = true;
                    return true;
                }
            }
            else
            {
                //We only update nextAttackString if 
                String tempAttack = NextAttackBuffer();

                if(nextAttack == null || tempAttack != null)
                {
                    nextAttack = tempAttack;
                    //Debug.Log("Next Attack: " + nextAttack + " in " + attackName);
                }
            }
        }
        return false;
    }

    /**
     *  Reads input and returns the result. This method must be overrided for each attack state
     * */
    protected String NextAttackBuffer()
    {
        String result = null;
        if (InputManager.LightAttack())
        {
            result = "lightAttack";
        }
        else if (InputManager.HeavyAttack())
        {
            result = "heavyAttack";
        }
        else if (InputManager.SpecialAttack())
        {
            switch (playerController.SpecialAttackToPerform())
            {
                case PlayerStanceType.STANCE_BLUE:
                    result = "blueSpecialAttack";
                    break;
                case PlayerStanceType.STANCE_RED:
                    result = "redSpecialAttack";
                    break;
            }
        }

        //If we have some input but is not available for current state; result must be null
        if ((availableAttacks == null || result != null) && availableAttacks.Contains(result) == false)
        {
            result = null;
        }

        return result;
    }

    //This method must be override for attack states. It should initialize all necessary data 
    virtual protected void LoadStateSettings()
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
