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


    public void Start()
    {
        bossManager = GameObject.FindGameObjectWithTag("Boss").GetComponentInParent<BossManager>();
        //canon = canon.GetComponent<Animator>();
    }

    public override void Run()
    {
        if (CanInteract())
        {
            activated = true;
            StartCoroutine(Attack());
            glow.GetComponent<ParticleSystem>().Play();
            StartCoroutine(LaserParticles());
        }
    }


    public override bool CanInteract()
    {
        return !activated;
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
    }


}
