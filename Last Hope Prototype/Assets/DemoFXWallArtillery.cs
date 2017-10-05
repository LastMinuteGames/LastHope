using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFXWallArtillery : MonoBehaviour {


    [SerializeField]
    private bool start = false;

    [SerializeField]
    private GameObject glow;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Animator canon;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = true;
        }
		if (start)
        {
            PlayTest();
            start = false;
        }
	}

    void PlayTest()
    {
        glow.GetComponent<ParticleSystem>().Play();
        StartCoroutine(LaserParticles());
    }

    IEnumerator LaserParticles()
    {
        yield return new WaitForSeconds(2.0f);
        laser.GetComponent<ParticleSystem>().Play();
        canon.SetTrigger("Shoot");
    }
}
