using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEnemyMoveAroundtState : StateMachineBehaviour {

    EnemyTrash enemyTrash;
    private int attackProbability = 0;
    private int approachProbability = 0;
    private float startTime = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyTrash == null)
        {
            enemyTrash = animator.transform.gameObject.GetComponent<EnemyTrash>();
            enemyTrash.nav.Stop();
        }

        attackProbability = enemyTrash.attackProbability;
        approachProbability = enemyTrash.approachProbability;
        startTime = Time.time;
        enemyTrash.transform.RotateAround(enemyTrash.target.transform.position, Vector3.up, enemyTrash.combatAngularSpeed * Time.deltaTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            if (Time.time - startTime >= 1)
            {
                int probability = UnityEngine.Random.Range(0, 100);
                bool change = true;
                if (enemyTrash.nav.remainingDistance > enemyTrash.attackRange)
                {
                    //enemyTrash.nav.SetDestination(enemyTrash.target.position);
                    //enemyTrash.nav.Resume();
                    animator.SetBool("moveAround", false);
                    animator.SetTrigger("moveForward");
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
                    animator.SetBool("moveAround", false);

                startTime = Time.time;
            }
            else
            {
                //enemyTrash.nav.Stop();
                enemyTrash.transform.RotateAround(enemyTrash.target.transform.position, Vector3.up, enemyTrash.combatAngularSpeed * Time.deltaTime);
            }
            //return type;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyTrash.transform.RotateAround(enemyTrash.target.transform.position, Vector3.up, enemyTrash.combatAngularSpeed * Time.deltaTime);
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
