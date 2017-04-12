using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            if (InputManager.Interact())
            {
                Debug.Log("Interact!!");
            }
        }
    }
}
