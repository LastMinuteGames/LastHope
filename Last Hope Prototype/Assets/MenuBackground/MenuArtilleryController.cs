using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuArtilleryController : MonoBehaviour
{
    
    public ParticleSystem leftBarrelParticles;
    public ParticleSystem rightBarrelParticles;

    void Start()
    {

    }

    void LeftBarrelShoot()
    {
        if (leftBarrelParticles != null)
        {
            leftBarrelParticles.Play();
        }
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Shot);
    }
    void RightBarrelShoot()
    {
        if (rightBarrelParticles != null)
        {
            rightBarrelParticles.Play();
        }
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Shot);
    }

    void MovementSound()
    {
        //AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Movement);
    }
}
