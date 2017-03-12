using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashIdleState : TrashState
{
    public TrashIdleState(GameObject go) : base(go)
    {
    }

    public override IEnemyState UpdateState()
    {
        return null;
    }
}
