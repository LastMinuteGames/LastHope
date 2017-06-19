using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossManager : MonoBehaviour
{
    public BossPhase[] bossPhases;
    private int currentPhaseId;
    private BossPhase currentPhase;

    void Start()
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



}
