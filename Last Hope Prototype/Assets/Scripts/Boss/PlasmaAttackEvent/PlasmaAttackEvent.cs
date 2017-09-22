using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/PlasmaAttackEvent")]
public class PlasmaAttackEvent : BossEvent
{
    private GameObject boss;
    private Animator bossAC;
    private GameObject plasmaRay;



    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting PlasmaAttackEvent");

        if (boss == null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
            bossAC = boss.GetComponent<Animator>();
            plasmaRay = boss.transform.Find("Root/Neck/Head/PlasmaRay").gameObject;
        }

        bossAC.SetTrigger("plasmaAttack");
    }


    public override bool UpdateEvent()
    {
        //Add behaviour
        //Debug.Log("update PlasmaAttackEvent");
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        EndRay();
        Debug.Log("terminating PlasmaAttackEvent");
    }


    public void StartRay()
    {
        plasmaRay.SetActive(true);

    }



    public void EndRay()
    {
        plasmaRay.SetActive(false);
    }
}
