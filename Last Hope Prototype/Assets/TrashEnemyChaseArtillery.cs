using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEnemyChaseArtillery : StateMachineBehaviour {
    EnemyTrash enemyTrash;
    PlayerDetection playerDetection;
    float artilleryRadius;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyTrash == null)
        {
            enemyTrash = animator.transform.gameObject.GetComponent<EnemyTrash>();
        }
        if (playerDetection == null)
        {
            playerDetection = animator.transform.gameObject.GetComponentInChildren<PlayerDetection>();
            artilleryRadius = playerDetection.artillery.GetComponent<CapsuleCollider>().radius;
        }
        animator.SetBool("chase", false);
        Debug.Log("Chase Artillery Enter");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Chase Artillery Update");
        if (enemyTrash.GetTarget() != null)
        {
            if (enemyTrash.nav.remainingDistance >= artilleryRadius + enemyTrash.combatRange)
            {
                enemyTrash.nav.SetDestination(enemyTrash.GetTarget().position);
                animator.ResetTrigger("attackArtillery");
                enemyTrash.nav.Resume();
            }
            else
            {
                animator.SetBool("chaseArtillery", false);
                animator.SetTrigger("attackArtillery");
                enemyTrash.nav.Stop();
            }
        }
        else
        {
            animator.SetBool("chaseArtillery", false);
            animator.SetBool("iddle", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Chase Artillery exit");
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
