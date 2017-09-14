using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownElevator : MonoBehaviour {

    public Animator elevator;
    private bool done = false;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (!done && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            done = true;
            elevator.speed = 0.2f;
        }
    }
}
