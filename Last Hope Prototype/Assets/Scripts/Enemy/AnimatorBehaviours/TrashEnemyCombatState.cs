using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEnemyCombatState : StateMachineBehaviour {
    private int attackProbability = 0;
    private int approachProbability = 0;
    EnemyTrash enemyTrash;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemyTrash == null)
        {
            enemyTrash = animator.transform.gameObject.GetComponent<EnemyTrash>();
        }
        attackProbability = enemyTrash.attackProbability;
        approachProbability = enemyTrash.approachProbability;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyTrash != null && enemyTrash.target != null && enemyTrash.target.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bool change = true;
            if (enemyTrash.nav.remainingDistance >= enemyTrash.combatRange)
            {
                animator.SetTrigger("chase");
            }
            else if (enemyTrash.nav.remainingDistance > enemyTrash.attackRange)
            {
                enemyTrash.nav.Stop();
                animator.SetTrigger("moveAround");
            }
            else if (enemyTrash.nav.remainingDistance <= enemyTrash.attackRange)
            {
                enemyTrash.nav.Stop();
                animator.SetTrigger("attack");
            }
            else
            {
                change = false;
            }

            if(change)
                animator.SetBool("combat", false);
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
