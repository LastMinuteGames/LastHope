﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrash : MonoBehaviour//: Enemy
{

    public int attackProbability;
    public int approachProbability;
    //public int moveAroundPlayerProbability
    public Transform enemy;
    public int life;
    public int maxLife;
    public bool dead = false;
    public long timeToAfterDeadMS;
    public long timeAttackRefresh;
    public int attack;
    public int combatRange;
    public int attackRange;
    public Collider katana;
    public int chaseSpeed;
    public int combatAngularSpeed;
    public int frameUpdateInterval;

    [HideInInspector]
    public double lastAttackTime = 0;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent nav;
    [HideInInspector]
    public Animator anim;

    Attack lastAttackReceived;


    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = chaseSpeed;
        anim = GetComponent<Animator>();
        anim.SetBool("iddle", true);
    }

    void Update()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            PlayerController playerScript = other.gameObject.GetComponentInParent<PlayerController>();
            Attack currentAttackReceived = playerScript.GetAttack();
            if(lastAttackReceived == null || currentAttackReceived.name != lastAttackReceived.name)
            {
                TakeDamage(currentAttackReceived.damage);
            }
            lastAttackReceived = currentAttackReceived;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ChangeTarget(other.transform);
            anim.SetBool("iddle", false);
            anim.SetTrigger("chase");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            nav.Stop();
            //this.target = null;
            anim.SetBool("iddle", true);
        }
    }

    public void TakeDamage(int damage)
    {
        if(life > 0)
        {
            life -= damage;
            anim.SetTrigger("damaged");
        }
    }

    public bool IsDead()
    {
        Debug.Log("Current life is: " + life);
        return life <= 0;
    }

    public void RecoveryHealth(int quantity)
    {
        life += quantity;
        if (life > maxLife)
            life = maxLife;
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
        if (this.target != null)
            nav.SetDestination(this.target.position);
    }

    public void Dead()
    {
        /**
         * TODO: Drop items if necessary
        **/
        Destroy(gameObject);
    }

    public void ClearLastAttackReceived()
    {
        lastAttackReceived = null;
    }
}