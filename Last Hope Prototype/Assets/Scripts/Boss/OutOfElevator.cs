using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfElevator : MonoBehaviour {

    public MeshCollider fence;
    public Animator wall;
    private bool closed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (!closed && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            closed = true;
            Debug.Log("Cierra!");
            fence.enabled = true;
            wall.SetBool("close", true);
        }
    }
}
