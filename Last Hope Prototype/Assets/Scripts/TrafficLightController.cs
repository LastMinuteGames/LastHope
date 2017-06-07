using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour {

    public Material emissiveRed;
    public Material emissiveYellow;
    public Material emissiveGreen;

    private Renderer rend;
    [SerializeField]
    private float currentTime;
    private float redTime;
    private float yellowTime;
    private float greenTime;
    private float maxTime;

    // Use this for initialization
    void Start () {
        redTime = 10;
        yellowTime = 7;
        greenTime = 0;
        maxTime = 15;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = emissiveRed;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            rend.sharedMaterial = emissiveGreen;
        }
        else if (currentTime >= redTime)
        {
            rend.sharedMaterial = emissiveRed;
        }
        else if (currentTime >= yellowTime)
        {
            rend.sharedMaterial = emissiveYellow;
        }
        else if (currentTime >= greenTime)
        {
            rend.sharedMaterial = emissiveGreen;
        }
    }
}
