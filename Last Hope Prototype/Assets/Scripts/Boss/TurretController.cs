using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Interactable
{

    private BossManager bossManager;
    private bool activated = false;


    public void Start()
    {
        bossManager = GameObject.FindGameObjectWithTag("Boss").GetComponentInParent<BossManager>();
    }

    public override void Run()
    {
        if (CanInteract())
        {
            activated = true;
            StartCoroutine(Attack());
            
        }
    }


    public override bool CanInteract()
    {
        return !activated;
    }

    IEnumerator Attack()
    {
        //print(Time.time);
        yield return new WaitForSeconds(0.5f);
        if (bossManager)
        {
            bossManager.TurretAttack();
        }
    }


}
