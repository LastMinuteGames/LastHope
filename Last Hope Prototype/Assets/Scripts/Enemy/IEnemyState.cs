using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface IEnemyState
{
    IEnemyState UpdateState();

    void OnTriggerEnter(GameObject go, Collider other);

    void OnTriggerExit(GameObject go, Collider other);
}

