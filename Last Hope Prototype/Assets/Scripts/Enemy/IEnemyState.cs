using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface IEnemyState
{
    IEnemyState UpdateState();

    void StartState();

    void EndState();

    void OnTriggerEnter(Collider other);

    void OnTriggerExit(Collider other);

    //void OnPlayerDetected(Collider other);

    void OnPlayerInRange(Collider other);

}

