using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/ArmAttackEvent")]
public class ArmAttackEvent : BossEvent
{
    private Animator bossAC;
	private bool started = false;


    public override void StartEvent()
    {
		started = false;
        base.StartEvent();
        BossManager.instance.SetEmisiveRed();
        Debug.Log("starting ArmAttackEvent");

        if (bossAC == null)
        {
            bossAC = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        }
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Boss_Arm_Attack);
    }


    public override bool UpdateEvent()
    {
		if (ellapsedTime >= anticipationTime && started == false) {
			started = true;
			bossAC.SetTrigger("armAttack");
		}
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating ArmAttackEvent");
    }
}
