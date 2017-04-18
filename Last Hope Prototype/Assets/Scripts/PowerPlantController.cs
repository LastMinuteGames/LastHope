using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantController : MonoBehaviour {

    private bool bridgeDown = false;
    private bool canActivateBridge = false;
    public GameObject bridgeFloor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (canActivateBridge)
        {
            if (InputManager.Interact())
            {
                bridgeDown = true;
                Debug.Log("Power plant charging...");
                Debug.Log("Wait for 5 seconds");
                canActivateBridge = false;
                Invoke("ActivateBridge", 5);
            }
        }
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

    void ActivateBridge()
    {
        bridgeFloor.GetComponent<BoxCollider>().isTrigger = true;
        Debug.Log("Puente bajado!");
    }

}
