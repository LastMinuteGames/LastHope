using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour {
    private UnityEngine.AI.NavMeshAgent nav;
    public Transform enemy;
    [HideInInspector]
    public IEnemyState currentState;
    public int life;
    public int maxLife;
    public long timeToAfterDeadMS;

    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        IEnemyState newState = currentState.UpdateState();
        if (newState != null)
        {
            currentState.EndState();
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
}
