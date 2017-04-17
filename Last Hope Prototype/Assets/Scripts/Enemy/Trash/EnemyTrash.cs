using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrash : Enemy
{

    public int attackProbability;
    public int approachProbability;
    //public int moveAroundPlayerProbability;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = chaseSpeed;

        attackZone.SetActive(false);
        states = new Dictionary<TrashStateTypes, IEnemyState>();

        IEnemyState state = null;

        state = new TrashIdleState(this.gameObject);
        states.Add(state.Type(), state);
        TrashStateTypes defaultState = state.Type();

        state = new TrashDeadState(this.gameObject);
        states.Add(state.Type(), state);

        state = new TrashDamagedState(this.gameObject);
        states.Add(state.Type(), state);

        state = new TrashChaseState(this.gameObject);
        states.Add(state.Type(), state);

        state = new TrashEnemyAttack(this.gameObject);
        states.Add(state.Type(), state);

        state = new TrashCombatState(this.gameObject);
        states.Add(state.Type(), state);

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
