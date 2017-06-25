using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossManager : MonoBehaviour
{
    public BossPhase[] bossPhases;
    public BossPhase currentPhase;
    public Slider hpSlider;

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
        UpdateHpSlider();
        Debug.Log("so much aww");

    }

    void BossDeath()
    {
        isDead = true;
        Debug.Log("boss is dead oh nooooo");
    }

    void UpdateHpSlider()
    {

        hpSlider.value = 100 * (bossPhases.Length - currentPhaseId) / (float)bossPhases.Length;
    }



}
