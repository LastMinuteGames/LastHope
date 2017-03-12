using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashIdleState : TrashState
{
    
    public override IEnemyState UpdateState()
    {
        return null;
    }

    /**
     * TODO: Delete if we have not need to change it!
     * */
    //public override void OnTriggerEnter(GameObject go, Collider other)
    //{
    //    if(other.tag == "PlayerAttack")
    //    {
    //        go.GetComponent<TrashStatePattern>().currentState = new TrashDamagedState();
    //    }
    //}

    //public override void OnTriggerExit(GameObject go, Collider other)
    //{
    //    if (other.tag == "PlayerAttack")
    //    {
    //        go.GetComponent<TrashStatePattern>().currentState = new TrashIdleState();
    //    }
    //}

}
