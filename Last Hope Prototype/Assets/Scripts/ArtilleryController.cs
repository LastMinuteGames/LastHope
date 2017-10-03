using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArtilleryController : TargetController
{
    [HideInInspector]
    public ParticleSystem leftBarrelParticles;
    public ParticleSystem rightBarrelParticles;

    void LeftBarrelShoot()
    {
        if (leftBarrelParticles != null)
        {
            leftBarrelParticles.Play();
        }
        //AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Shot);
		AudioSources.instance.Play3DSound((int)AudiosSoundFX.Environment_Artillery_Shot, transform.position, 0.97f, AudioRolloffMode.Linear, 0.35f);

    }
    void RightBarrelShoot()
    {
        if (rightBarrelParticles != null)
        {
            rightBarrelParticles.Play();
        }
        //AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Shot);
        AudioSources.instance.Play3DSound((int)AudiosSoundFX.Environment_Artillery_Shot, transform.position, 0.97f, AudioRolloffMode.Linear, 0.35f);

    }

    void MovementSound()
    {
        //AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Movement);
        AudioSources.instance.Play3DSound((int)AudiosSoundFX.Environment_Artillery_Movement, transform.position, 1f, AudioRolloffMode.Linear, 0.3f);
    }
}
