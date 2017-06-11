using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehaviour
{
    EB_ARTILLERY,
    EB_DEFAULT
}

public enum TargetType
{
    TT_PLAYER,
    TT_ARTILLERY
}

public class Target
{
    public Transform transf;
    public TargetType type;
}

public class EnemyTrash : MonoBehaviour//: Enemy
{
    public EnemyBehaviour behaviour;
    //public int moveAroundPlayerProbability
    public Transform enemy;
    public int life;
    public int maxLife;
    public bool dead = false;
    public int combatRange;
    public int attackRange;
    public Collider katana;
    public MeleeWeaponTrail swordEmitter;
    public GameObject deadParticles;
    public int chaseSpeed;
    public int combatAngularSpeed;
    public GameObject lifeDrop;
    public int lifeDropProbability;
    public GameObject EnergyDrop;
    public int energyDropProbability;

    [HideInInspector]
    public double lastAttackTime = 0;
    private Target target;
    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent nav;
    [HideInInspector]
    public Animator anim;

    private Attack lastAttackReceived;
    private Dictionary<String, Attack> enemyAttacks;
    private Attack currentAttack;

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

        enemyAttacks = new Dictionary<string, Attack>();
        enemyAttacks.Add("Attack", new Attack("Attack", 10));

        target = new Target();
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            PlayerController playerScript = other.gameObject.GetComponentInParent<PlayerController>();
            AnimatorStateInfo currentState = playerScript.anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("H1") || currentState.IsName("H2") || currentState.IsName("H3"))
            {
                playerScript.HeavyAttackEffect();
            }

            Attack currentAttackReceived = playerScript.GetAttack();
            if (currentAttackReceived != null)
            {
                if (lastAttackReceived == null || currentAttackReceived.name != lastAttackReceived.name)
                {
                    if (!currentState.IsName("RedSpecialAttack"))
                    {
                        playerScript.SpawnHitParticles(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
                    }
                    TakeDamage(currentAttackReceived.damage);
                }
                lastAttackReceived = currentAttackReceived;
            }
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

    public void ChangeTarget(Target target)
    {
        this.target = target;
        if (this.target != null && nav != null)
            nav.SetDestination(this.target.transf.position);
    }

    public void ChangeTarget(Transform transf, TargetType type)
    {
        target.transf = transf;
        target.type = type;
        if (target != null && nav != null)
            nav.SetDestination(target.transf.position);
    }

    public Target GetTarget()
    {
        return target;
    }

    public void Dead()
    {
        /**
         * TODO: Drop items if necessary
        **/
        float lifeRandomNumber =  UnityEngine.Random.Range(0, 100.0f);
        float energyRandomNumber = UnityEngine.Random.Range(0, 100.0f);

        if(lifeRandomNumber < lifeDropProbability)
        {
            Instantiate(lifeDrop, transform.position, Quaternion.identity);
        }

        if(energyRandomNumber < energyDropProbability)
        {
            Instantiate(EnergyDrop, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    public void ClearLastAttackReceived()
    {
        lastAttackReceived = null;
    }

    public Attack GetAttack()
    {
        return currentAttack == null ? null : enemyAttacks[currentAttack.name];
    }

    public void ChangeAttack(string name)
    {
        currentAttack = enemyAttacks[name];
    }

    public void StartAttack()
    {
        katana.enabled = true;
    }

    public void EndAttack()
    {
        katana.enabled = false;
    }

    public void ClearAnimatorParameters()
    {
        anim.SetBool("combat", false);
        anim.SetBool("iddle", false);
        anim.SetBool("walk", false);
        anim.SetBool("chase", false);
        anim.SetBool("moveAround", false);
        anim.SetBool("moveForward", false);
    }

    public void EnableSwordEmitter()
    {
        swordEmitter.Emit = true;
    }

    public void DisableSwordEmitter()
    {
        swordEmitter.Emit = false;
    }
    
    public void SpawnDeadParticles()
    {
        GameObject particle = Instantiate(deadParticles, transform.position + new Vector3(0,1,0), transform.rotation);
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        float totalDuration = ps.main.duration + ps.main.startLifetime.constantMax;
        Destroy(particle, totalDuration);
    }
}
