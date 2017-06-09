using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEnemyMoveForwardState : StateMachineBehaviour {

    EnemyTrash enemyTrash;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("MoveForward Enter");
        if (enemyTrash == null)
        {
            enemyTrash = animator.transform.gameObject.GetComponent<EnemyTrash>();
        }
        enemyTrash.nav.speed = 5;
        enemyTrash.nav.Resume();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("MoveForward Update");
        if (enemyTrash.nav.remainingDistance <= enemyTrash.attackRange)
        {
            enemyTrash.nav.Stop();
            animator.SetBool("moveForward", false);
            animator.SetTrigger("attack");
            //return enemyTrashTypes.ATTACK_STATE;
        } else if (enemyTrash.nav.remainingDistance > enemyTrash.combatRange)
        {
            animator.SetBool("moveForward", false);
            animator.SetBool("chase", true);
        }
        else
        {
            enemyTrash.nav.Resume();
            if (enemyTrash.GetTarget() != null)
                enemyTrash.nav.SetDestination(enemyTrash.GetTarget().position);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
