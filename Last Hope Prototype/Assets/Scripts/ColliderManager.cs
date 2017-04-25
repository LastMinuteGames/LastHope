using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour {

    private bool debug; 

	// Use this for initialization
	void Start () {
        debug = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().debugMode;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().debugMode != debug) {
            debug = !debug;
            ManageColliders();
        }
    }

    void ManageColliders() {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.activeInHierarchy)
            {
                if (go.name.StartsWith("Deco_"))
                {
                    go.GetComponent<MeshRenderer>().enabled = !go.GetComponent<MeshRenderer>().enabled;
                    //Gizmos
                }
                if (go.name.StartsWith("Col_"))
                {
                    go.GetComponent<MeshRenderer>().enabled = !go.GetComponent<MeshRenderer>().enabled;
                }
            }
        }
    }

}
