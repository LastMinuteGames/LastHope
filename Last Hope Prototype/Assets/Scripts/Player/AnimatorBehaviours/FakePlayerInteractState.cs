using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerInteractState : StateMachineBehaviour
{
    PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerController == null)
        {
            playerController = animator.transform.gameObject.GetComponent<PlayerController>();
        }
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_PlayerToWorld_Interact);
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //If get damage before animations ends, cannot interact!
        if (animator.GetNextAnimatorStateInfo(0).IsName("Damage") == false){
            int layerMask = 1 << LayerMask.NameToLayer("Interactable");
            Collider[] colliders = Physics.OverlapSphere(playerController.transform.position, 1, layerMask);
            Interactable interactable = null;
            foreach (Collider col in colliders)
            {
                interactable = col.gameObject.GetComponent<Interactable>();
                if (interactable != null && interactable.CanInteract())
                {
                    interactable.Run();
                    playerController.canInteract = false;
                    break;
                }
            }
        }
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
