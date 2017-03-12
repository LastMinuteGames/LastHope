using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashState : IEnemyState
{
    public virtual IEnemyState UpdateState()
    {
        return null;
    }

    public virtual void OnTriggerEnter(GameObject go, Collider other)
    {
        if(other.tag == "PlayerAttack")
        {
            go.GetComponent<TrashStatePattern>().currentState = new TrashDamagedState();
        }
    }

    public virtual void OnTriggerExit(GameObject go, Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            go.GetComponent<TrashStatePattern>().currentState = new TrashIdleState();
        }
    }

}
