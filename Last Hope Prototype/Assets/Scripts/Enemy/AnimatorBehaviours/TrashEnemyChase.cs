using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEnemyChase : StateMachineBehaviour {
    EnemyTrash enemyTrash;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemyTrash == null)
        {
            enemyTrash = animator.transform.gameObject.GetComponent<EnemyTrash>();
        }
        Debug.Log("Chase Enter");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Chase Update");
        RaycastHit hit;
        Vector3 direction;
        bool rayHit;
        Color rayColor;
        switch (enemyTrash.GetTarget().type)
        {
            case TargetType.TT_PLAYER:
                // Cast ray to target in combat range
                direction = enemyTrash.GetTarget().transf.position - enemyTrash.transform.position;
                rayHit = Physics.Raycast(enemyTrash.transform.position, direction, out hit, enemyTrash.combatRange);

                // Debug draw ray
                rayColor = rayHit ? Color.green : Color.red;
                Debug.DrawRay(enemyTrash.transform.position, direction, rayColor);

                if (!rayHit)
                {
                    enemyTrash.nav.SetDestination(enemyTrash.GetTarget().transf.position);
                    enemyTrash.nav.Resume();
                }
                else
                {
                    animator.SetBool("chase", false);
                    animator.SetBool("combat", true);
                    enemyTrash.nav.Stop();
                }
                break;
            case TargetType.TT_ARTILLERY:
                // Cast ray to target in attack range
                direction = enemyTrash.GetTarget().transf.position - enemyTrash.transform.position;
                rayHit = Physics.Raycast(enemyTrash.transform.position, direction, out hit, enemyTrash.attackRange);

                // Debug draw ray
                rayColor = rayHit ? Color.green : Color.red;
                Debug.DrawRay(enemyTrash.transform.position, direction, rayColor);

                if (!rayHit)
                {
                    enemyTrash.nav.SetDestination(enemyTrash.GetTarget().transf.position);
                    enemyTrash.nav.Resume();
                }
                else
                {
                    animator.SetBool("chase", false);
                    animator.SetBool("attackArtillery", true);
                    enemyTrash.nav.Stop();
                }
                break;
            default:
                animator.SetBool("chase", false);
                animator.SetBool("iddle", true);
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Chase exit");
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
