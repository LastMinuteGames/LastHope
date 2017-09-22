using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/ArmAttackEvent")]
public class ArmAttackEvent : BossEvent
{
    private Animator bossAC;

    public override void StartEvent()
    {
        base.StartEvent();
        BossManager.instance.SetEmisiveFist();
        Debug.Log("starting ArmAttackEvent");

        if (bossAC == null)
        {
            bossAC = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>();
        }

        bossAC.SetTrigger("armAttack");
    }


    public override bool UpdateEvent()
    {
        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        base.TerminateEvent();
        Debug.Log("terminating ArmAttackEvent");
    }
}
