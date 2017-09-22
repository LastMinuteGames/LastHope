using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Interactable
{
    [SerializeField]
    private GameObject glow;
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Animator canon;
    private BossManager bossManager;
    private bool activated = false;

    [SerializeField]
    private Material baseColor;
    [SerializeField]
    private Texture emissiveOff;
    [SerializeField]
    private Texture emissiveOn;

    [SerializeField]
    private float turretRotation;
    [SerializeField]
    private Transform rot;
    private float initialRot = 0;
    private bool rotating = false;
    [SerializeField]
    private float speed = 0.5f;
    private Vector3 velocity;

    private bool doOnce = false;

    public void Start()
    {
        baseColor.SetTexture("_EmissionMap", emissiveOn);
        bossManager = GameObject.FindGameObjectWithTag("Boss").GetComponentInParent<BossManager>();
        initialRot = rot.transform.localEulerAngles.z;
    }

    public void Restart()
    {
        activated = false;
        initialRot = 0;
        rotating = false;
        doOnce = false;
        baseColor.SetTexture("_EmissionMap", emissiveOn);
        initialRot = rot.transform.localEulerAngles.z;
    }

    void FixedUpdate()
    {
        if (rotating)
        {
            RotateTurret();
        }
    }

    public override void Run()
    {
        if (CanInteract())
        {
            activated = true;
            StartCoroutine(Attack());
            glow.GetComponent<ParticleSystem>().Play();
            StartCoroutine(LaserParticles());
            rotating = true;
        }
    }


    public override bool CanInteract()
    {
        return !activated;
    }

    private void RotateTurret()
    {
        if (turretRotation >= 0)
        {
            if (rot.transform.localEulerAngles.z >= initialRot + turretRotation)
            {
                rotating = false;
            }
        }
        else
        {
            if (!doOnce)
            {
                rot.Rotate(new Vector3(0, 0, 1), -1);
                doOnce = true;
            }
            if (rot.transform.localEulerAngles.z < initialRot + 360 - Math.Abs(turretRotation))
            {
                rotating = false;
            }
        }
        velocity = Vector3.Lerp(new Vector3(0, 0, initialRot), new Vector3(0, 0, turretRotation), speed * Time.deltaTime);
        rot.Rotate(new Vector3(0, 0, 1), velocity.z);

    }

    IEnumerator LaserParticles()
    {
        yield return new WaitForSeconds(2.0f);
        laser.GetComponent<ParticleSystem>().Play();
        canon.SetTrigger("Shoot");
    }

    IEnumerator Attack()
    {
        //print(Time.time);
        yield return new WaitForSeconds(3.0f);
        if (bossManager)
        {
            bossManager.TurretAttack();
        }
        baseColor.SetTexture("_EmissionMap", emissiveOff);
    }


}
