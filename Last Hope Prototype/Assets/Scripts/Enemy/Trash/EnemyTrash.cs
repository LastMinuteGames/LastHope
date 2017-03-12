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
    }
	
    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }
}
