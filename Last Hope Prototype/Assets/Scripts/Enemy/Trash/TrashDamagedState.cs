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
}
