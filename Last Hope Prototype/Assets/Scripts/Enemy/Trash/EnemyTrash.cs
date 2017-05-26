using System;
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
    public IEnemyState currentState;
    private IEnemyState previousState;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent nav;
    [HideInInspector]
    public Animator anim;

    private static readonly int iddleState = Animator.StringToHash("Iddle");
    private static readonly int chaseState = Animator.StringToHash("Chase");
    private static readonly int damageState = Animator.StringToHash("Damage");
    private static readonly int dieState = Animator.StringToHash("Die");
    private static readonly int deadState = Animator.StringToHash("Dead");
    private static readonly int inCombatState = Animator.StringToHash("Combat");
    private static readonly int moveBackState = Animator.StringToHash("MoveBack");
    private static readonly int moveForwardState = Animator.StringToHash("MoveForward");
    private static readonly int moveAroundState = Animator.StringToHash("MoveAround");
    private static readonly int AttackState = Animator.StringToHash("Attack");



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
        int currentState = anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
        if (currentState == iddleState)
        {
            
        } else if(currentState == chaseState)
        {

        }
        //Cant use switch; must to find a pass around to const compilation error
        /*switch (anim.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            case iddleState:
                break;
            default:
                break;
        }*/
    }

    public void OnTriggerEnter(Collider other)
    {
        //currentState.OnTriggerEnter(other);
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            //if (anim.GetCurrentAnimatorStateInfo(0).IsName("damage"))
            //{
                //trashState.ChangeState(TrashStateTypes.DAMAGED_STATE);

                /**
                 *  TODO: Get damage from player!
                **/
                int damage = 10;
                TakeDamage(damage);
                //trashState.TakeDamage(damage);
            //}
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //this.target = other.transform;
            ChangeTarget(other.transform);
            anim.SetBool("iddle", false);
            anim.SetTrigger("chase");
            //trashState.ChangeState(TrashStateTypes.CHASE_STATE);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            nav.Stop();
            this.target = null;
            anim.SetBool("iddle", true);
        }
    }

    public void TakeDamage(int damage)
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Damage") == false)
        {
            life -= damage;
            anim.SetTrigger("damaged");

        }
        //if(life <= 0)
        //{
        //    anim.SetBool("dead", true);
        //}
        //else
        //{
        //}
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
}
