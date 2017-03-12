using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashStatePattern : EnemyStatePattern {

    public int life;
    public int maxLife;

    [HideInInspector]
    public TrashState currentState;


    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        currentState = new TrashIdleState();
    }
	
	// Update is called once per frame
	void Update () {
        TrashState newState = (TrashState)currentState.UpdateState();
        if(newState != null)
        {
            currentState = newState;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this.gameObject, other);
    }

    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this.gameObject, other);
    }
}
