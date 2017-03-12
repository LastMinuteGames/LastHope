using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool debugMode = false;
    public Camera[] cameras;
    public Camera activeCamera;
    private int camToActivate;
    private int currentIndex;

    void Start()
    {
        ActivateCamera(0);
    }


    void Update()
    {
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                camToActivate = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                camToActivate = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                camToActivate = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                camToActivate = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                camToActivate = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                camToActivate = 5;
            }
        }
        else
        {

            camToActivate = 0;
        }
    }

    void LateUpdate()
    {
        if (camToActivate != currentIndex)
        {
            ActivateCamera(camToActivate);
        }
    }

    void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (i == index)
            {
                cameras[i].enabled = true;
                cameras[i].GetComponent<AudioListener>().enabled = true;
                activeCamera = cameras[i];
                currentIndex = i;
            }
            else
            {
                cameras[i].enabled = false;
                cameras[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }
}
