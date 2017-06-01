using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlantController : Interactable {

    private bool running = false;
    public GameObject bridge;
    public GameObject bridgeFloor;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    public override void Run()
    {
        if(CanInteract())
        {
            //TODO: Hide message
            Debug.Log("Power plant charging...");
            Debug.Log("Wait for 5 seconds");
            running = true;
            Invoke("ActivateBridge", 5);
        }
    }

    public override bool CanInteract()
    {
        return !running;
    }

    void OnTriggerEnter(Collider other)
    {
        if (running == false && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TODO: Show message in screen
            Debug.Log("Press E to activate the bridge.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TODO: Hide message
        }
    }

    void ActivateBridge()
    {
        bridgeFloor.GetComponent<BoxCollider>().isTrigger = true;
        Debug.Log("Puente bajado!");
        for (int i = 0; i < bridge.transform.childCount; ++i)
        {
            GameObject child = bridge.transform.GetChild(i).gameObject;
            if (child.activeInHierarchy)
            {
                if (child.name.Contains("Energy")) {
                    for (int j = 0; j < child.transform.childCount; ++j)
                    {
                        GameObject childOfChild = child.transform.GetChild(j).gameObject;
                        if (childOfChild.activeInHierarchy)
                        {
                            if (childOfChild.name.Contains("Deco"))
                            {
                                childOfChild.GetComponent<MeshRenderer>().enabled = true;
                                Debug.Log(childOfChild.name);
                            }
                        }
                            
                    }
                        
                }
            }
        }
    }

}
