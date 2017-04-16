using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrash : Enemy
{

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        attackZone.SetActive(false);
        states = new Dictionary<string, IEnemyState>();

        IEnemyState state = null;

        state = new TrashIdleState(this.gameObject);
        states.Add(state.GetName(), state);
        String defaultState = state.GetName();

        state = new TrashDeadState(this.gameObject);
        states.Add(state.GetName(), state);

        state = new TrashDamagedState(this.gameObject);
        states.Add(state.GetName(), state);

        state = new TrashChaseState(this.gameObject);
        states.Add(state.GetName(), state);

        state = new TrashEnemyAttack(this.gameObject);
        states.Add(state.GetName(), state);

        ChangeState(defaultState);
    }

    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }


}
