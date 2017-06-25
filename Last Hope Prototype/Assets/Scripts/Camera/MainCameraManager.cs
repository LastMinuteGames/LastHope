using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour {

    private FreeLookCam freeLookCam;
    private BossCam bossCam;


    void Awake ()
    {
        freeLookCam = GetComponent<FreeLookCam>();
        bossCam = GetComponent<BossCam>();
    }

    void Start ()
    {
        bossCam.enabled = false;
        freeLookCam.enabled = true;
    }

    public void SwapCameraMode ()
    {
        freeLookCam.enabled = !freeLookCam.enabled;
        bossCam.enabled = !bossCam.enabled;
    }

}
