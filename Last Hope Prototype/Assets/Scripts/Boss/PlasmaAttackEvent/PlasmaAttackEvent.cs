using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/PlasmaAttackEvent")]
public class PlasmaAttackEvent : BossEvent
{
    private GameObject boss;
    private Animator bossAC;
    private GameObject plasmaRay;
	private bool started = false;



    public override void StartEvent()
    {
		started = false;
        base.StartEvent();
        Debug.Log("starting PlasmaAttackEvent");

        if (boss == null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
            bossAC = boss.GetComponent<Animator>();
            plasmaRay = boss.transform.Find("Root/Neck/Head/PlasmaRay").gameObject;
        }
		BossManager.instance.SetEmisiveRed();
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Boss_Plasma_Attack);
    }


    public override bool UpdateEvent()
    {
		if (ellapsedTime >= anticipationTime && started == false) {
			started = true;
			bossAC.SetTrigger("plasmaAttack");
		}
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
