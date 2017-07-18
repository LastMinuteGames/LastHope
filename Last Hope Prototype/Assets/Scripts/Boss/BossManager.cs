using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BossManager : MonoBehaviour
{
    public BossPhase[] bossPhases;
    public BossPhase currentPhase;
    public Slider hpSlider;
    public GameObject canvasGO;
    public Transform camTargetT;
    public GameObject rocketSpawnManager;
    

    private int currentPhaseId;
    private bool isDead = false;
    private bool isAwaken = false;
    private Animator animator;
    private FreeLookCam freeLookCam;
    private BossCam bossCam;

    void Start()
    {
        //StartBossFight();
        animator = GetComponent<Animator>();
        freeLookCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FreeLookCam>();
        bossCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BossCam>();
        
    }

    public void StartBossFight()
    {
        Debug.Log("start boss fight");
        isAwaken = true;
        if (rocketSpawnManager != null)
        {
            GameObject prefab;
            prefab = (GameObject)Instantiate(rocketSpawnManager);
            prefab.name = "RocketSpawnManager";
        }
        canvasGO.SetActive(true);
        if (bossCam)
        {
            bossCam.StartFollowing();
        }
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
        animator.SetTrigger("isDead");
        isDead = true;
        isAwaken = false;
        canvasGO.SetActive(false);
        Debug.Log("boss is dead oh nooooo");
    }

    void UpdateHpSlider()
    {
        hpSlider.value = 100 * (bossPhases.Length - currentPhaseId) / (float)bossPhases.Length;
    }

    public void Dead()
    {
        Debug.Log("Dead animation event!");
        SceneManager.LoadScene("WinScreen");
    }

}
