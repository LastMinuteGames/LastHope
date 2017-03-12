using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TrashDamagedState : TrashState
{
    public TrashDamagedState(GameObject go) : base(go)
    {
    }

    public override IEnemyState UpdateState()
    {
        return null;
    }

    /**
     * TODO: Delete if we have not need to change it!
    * */
    //public override void OnTriggerEnter(GameObject go, Collider other)
    //{
    //    Debug.Log("Collider!");
    //    if (other.tag == "PlayerAttack")
    //    {

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
