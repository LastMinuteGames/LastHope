using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public Transform enemy;
    public int life;
    public int maxLife;
    public bool dead = false;
    public long timeToAfterDeadMS;
    public long timeAttackRefresh;
    public int attack;
    public int combatRange;
    public int attackRange;
    //public GameObject attackInRange;
    public GameObject attackZone;
    public int chaseSpeed;
    public int combatAngularSpeed;
    public int frameUpdateInterval;

    [HideInInspector]
    public double lastAttackTime = 0;
    [HideInInspector]
    public IEnemyState currentState;
    private IEnemyState previousState;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent nav;
    [HideInInspector]
    public Animator anim;
    protected Dictionary<TrashStateTypes, IEnemyState> states;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == null)
            return;
        TrashStateTypes newState = currentState.UpdateState();

        if (newState != TrashStateTypes.UNDEFINED_STATE && currentState.Type() != newState)
        {
            previousState = currentState;
            ChangeState(newState);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
    }

    public void RecoveryHealth(int quantity)
    {
        life += quantity;
        if (life > maxLife)
            life = maxLife;
    }

    public void Dead()
    {
        /**
         * TODO: Drop items if necessary
        **/
        Destroy(gameObject);
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
        Debug.Log("Changed Target!!!");
        if (this.target != null)
            nav.SetDestination(this.target.position);
    }

    public void OnPlayerDetected(Collider player)
    {
        ChangeTarget(player.gameObject.transform);
        currentState.OnTriggerEnter(player);
    }

    public void OnPlayerFlees(Collider player)
    {
        ChangeTarget(null);
        currentState.OnTriggerExit(player);
    }

    public void OnPlayerInRange(Collider player)
    {
        currentState.OnPlayerInRange(player);
    }

    public void Attack()
    {
        attackZone.SetActive(true);
    }

    public void ChangeToPreviousState()
    {
        if (previousState != null)
        {
            currentState.EndState();
            currentState = previousState;
            currentState.StartState();
        }
    }

    public void ChangeState(TrashStateTypes type)
    {
        if (states.ContainsKey(type))
        {
            if (currentState != null)
                currentState.EndState();

            currentState = states[type];
            currentState.StartState();
        }
    }
}
