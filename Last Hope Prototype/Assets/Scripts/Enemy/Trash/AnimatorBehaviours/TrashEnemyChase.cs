using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEnemyChase : StateMachineBehaviour
{
    EnemyTrash enemyTrash;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyTrash == null)
        {
            enemyTrash = animator.transform.gameObject.GetComponent<EnemyTrash>();
        }
        enemyTrash.DisableSwordEmitter();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RaycastHit attackHit;
        Vector3 direction;
        bool rayAttackHit;
        Color rayColor;
        var targetLayerMask = 1 << 12;
        if (enemyTrash.GetTarget().transf != null)
        {
            switch (enemyTrash.GetTarget().type)
            {
                case TargetType.TT_PLAYER:
                    // Cast ray to target in combat range
                    direction = enemyTrash.GetTarget().transf.position - enemyTrash.transform.position;
                    rayAttackHit = Physics.Raycast(enemyTrash.transform.position, direction, out attackHit, enemyTrash.combatRange, targetLayerMask);

                    // Debug draw ray
                    rayColor = rayAttackHit ? Color.green : Color.red;
                    Debug.DrawRay(enemyTrash.transform.position, direction, rayColor);

                    if (!rayAttackHit && !animator.GetBool("iddle"))
                    {
                        enemyTrash.nav.SetDestination(enemyTrash.GetTarget().transf.position);
                        enemyTrash.nav.Resume();
                    }
                    else if (rayAttackHit)
                    {
                        animator.SetBool("chase", false);
                        animator.SetBool("combat", true);
                        enemyTrash.nav.Stop();
                    }
                    break;
                case TargetType.TT_ARTILLERY:
                    // Cast ray to target in attack range
                    direction = enemyTrash.GetTarget().transf.position - enemyTrash.transform.position;
                    targetLayerMask = 1 << 19;
                    rayAttackHit = Physics.Raycast(enemyTrash.transform.position, direction, out attackHit, enemyTrash.attackRange, targetLayerMask);

                    // Debug draw ray
                    rayColor = rayAttackHit ? Color.green : Color.red;
                    Debug.DrawRay(enemyTrash.transform.position, direction, rayColor);
                    
                    // If not in attack range and have to go to the artillery calculate the target point out of the artillery and go there.
                    if (!rayAttackHit && !animator.GetBool("iddle"))
                    {
                        RaycastHit destinationHit;
                        Physics.Raycast(enemyTrash.transform.position, direction, out destinationHit, targetLayerMask);
                        enemyTrash.nav.SetDestination(destinationHit.point);
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
        else
        {
            animator.SetBool("chase", false);
            animator.SetBool("attackArtillery", false);
            animator.SetBool("combat", false);
            animator.SetBool("iddle", true);
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
