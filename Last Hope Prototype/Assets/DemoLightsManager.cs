using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLightsManager : MonoBehaviour {

    private List<GameObject> lights = new List<GameObject>();

    void Start () {

        foreach (Transform child in transform)
        {
            lights.Add(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleLights();
        }
	}

    void ToggleLights()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(!light.activeSelf);
        }
    }
}
