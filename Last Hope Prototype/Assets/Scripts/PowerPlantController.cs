using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantController : MonoBehaviour {

    private bool bridgeDown = false;
    private bool canActivateBridge = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (bridgeDown == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                canActivateBridge = true;
                Debug.Log("Press E to activate the bridge.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (bridgeDown == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                canActivateBridge = false;
            }
        }
    }

}
