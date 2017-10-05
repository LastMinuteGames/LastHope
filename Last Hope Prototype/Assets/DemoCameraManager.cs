using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCameraManager : MonoBehaviour {

    private List<GameObject> cameras = new List<GameObject>();


    // Use this for initialization
    void Start () {
        foreach (Transform child in transform)
        {
            cameras.Add(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Activate(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Activate(2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Activate(3);
        }
    }

    void Activate(int num)
    {
        foreach (GameObject particle in cameras)
        {
            particle.gameObject.SetActive(false);
        }
        cameras[num - 1].gameObject.SetActive(true);
    }
}
