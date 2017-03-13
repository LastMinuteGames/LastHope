using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour {


    public Transform enemy;
    public int life;
    public int maxLife;
    public long timeToAfterDeadMS;
    public long timeAttackRefresh;
    public int attack;
    public GameObject attackInRange;
    public GameObject attackZone;

    [HideInInspector]
    public IEnemyState currentState;
    private IEnemyState previousState;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent nav;

    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        attackZone.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        IEnemyState newState = currentState.UpdateState();
        if (newState != null)
        {
            currentState.EndState();
            previousState = currentState;
            currentState = newState;
            currentState.StartState();
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
        if(this.target != null)
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
}
