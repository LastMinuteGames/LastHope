using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrash : Enemy {

    void Awake()
    {
    }

	// Use this for initialization
	void Start ()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentState = new TrashIdleState(gameObject);
        anim = GetComponent<Animator>();
        //hand.animation["bridge"].speed = -1;
        //hand.animation["bridge"].time = hand.animation["bridge"].length;
        //hand.animation.Play("bridge");
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = "Test";
        //animationEvent.floatParameter = 0;
        animationEvent.time = 0.8f;
        //anim.GetClip("Die").AddEvent(animationEvent);
    }

    void Update()
    {
        if (!dead && life <= 0)
        {
            dead = true;
            anim.SetBool("die", true);
        }
    }
	
    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

}
