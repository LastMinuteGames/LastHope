using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossManager : MonoBehaviour
{
    public BossPhase[] bossPhases;
    public BossPhase currentPhase;

    private int currentPhaseId;
    private bool isDead = false;



    void Start()
    {
        StartBossFight();
    }

    public void StartBossFight()
    {
        Debug.Log("start boss fight");
        if (bossPhases.Length > 0)
        {
            currentPhaseId = 0;
            currentPhase = bossPhases[currentPhaseId];
            currentPhase.StartPhase();
        }
    }

    void Update()
    {
        //Debug.Log("boss update");
        currentPhase.UpdatePhase();

    }


    void TerminateCurrentPhase()
    {
        currentPhase.TerminatePhase();
        currentPhaseId++;
        if (currentPhaseId < bossPhases.Length)
        {
            currentPhase = bossPhases[currentPhaseId];
            currentPhase.StartPhase();
        }
        else
        {
            BossDeath();
        }
    }


    public void TurretAttack()
    {
        if (isDead)
        {
            return;
        }
        TerminateCurrentPhase();
        Debug.Log("so much aww");

    }

    void BossDeath()
    {
        isDead = true;
        Debug.Log("boss is dead oh nooooo");
    }



}
