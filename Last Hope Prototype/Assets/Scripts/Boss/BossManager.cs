using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossManager : MonoBehaviour
{
    public BossPhase[] bossPhases;
    public BossPhase currentPhase;
    public Slider hpSlider;
    public GameObject canvasGO;

    private int currentPhaseId;
    private bool isDead = false;
    private bool isAwaken = false;
    private Animator animator;



    void Start()
    {
        //StartBossFight();
        animator = GetComponent<Animator>();
    }

    public void StartBossFight()
    {
        Debug.Log("start boss fight");
        isAwaken = true;
        canvasGO.SetActive(true);
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
        if (isAwaken && !isDead)
        {
            currentPhase.UpdatePhase();
        }

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
        animator.SetTrigger("isDamaged");
        UpdateHpSlider();
        Debug.Log("so much aww");

    }

    void BossDeath()
    {
        isDead = true;
        isAwaken = false;
        canvasGO.SetActive(false);
        Debug.Log("boss is dead oh nooooo");
    }

    void UpdateHpSlider()
    {

        hpSlider.value = 100 * (bossPhases.Length - currentPhaseId) / (float)bossPhases.Length;
    }



}
