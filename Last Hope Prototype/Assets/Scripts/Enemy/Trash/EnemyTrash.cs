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
    }

    void Update()
    {
        if (!dead && life <= 0)
        {
            dead = true;
            anim.Play("Die");
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

    void Test()
    {
        Debug.Log("EVENTOO!!");
    }
}
