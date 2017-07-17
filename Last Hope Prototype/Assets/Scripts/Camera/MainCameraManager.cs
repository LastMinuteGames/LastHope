using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (FreeLookCam))]
[RequireComponent (typeof (BossCam))]
public class MainCameraManager : MonoBehaviour
{

    FreeLookCam freeLookCam;
    CameraCollision cameraCollision;
    CameraShake cameraShake;

    BossCam bossCam;

    void Awake()
    {
        freeLookCam = GetComponent<FreeLookCam>();
        cameraCollision = GetComponent<CameraCollision>();
        cameraShake = GetComponent<CameraShake>();

        bossCam = GetComponent<BossCam>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetBossCam();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            SetFreeCam();
        }
    }

    public void SetBossCam()
    {
        freeLookCam.enabled = false;
        cameraCollision.enabled = false;
        cameraShake.enabled = false;

        bossCam.enabled = true;
    }

    public void SetFreeCam()
    {
        freeLookCam.enabled = true;
        cameraCollision.enabled = true;
        cameraShake.enabled = true;

        bossCam.enabled = false;
    }


}
