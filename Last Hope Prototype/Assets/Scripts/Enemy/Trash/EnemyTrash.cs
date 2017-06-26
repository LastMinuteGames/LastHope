using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyTrash : Enemy //MonoBehaviour
{
    //public int moveAroundPlayerProbability
    public Transform enemy;

    public int combatRange;
    public int attackRange;
    public Collider katana;
    public MeleeWeaponTrail swordEmitter;
    public GameObject deadParticles;
    public int chaseSpeed;
    public int combatAngularSpeed;
    
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

        enemyType = EnemyType.ET_TRASH;
    }

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            //TODO: Use attacks instead of animator to know what attack has received...animator sometimes "lies"...
            PlayerController playerScript = other.gameObject.GetComponentInParent<PlayerController>();

            Attack currentAttackReceived = playerScript.GetAttack();
            if (currentAttackReceived != null)
            {
                Debug.Log("CurrentAttackReceived: " + currentAttackReceived.name);
                if(lastAttackReceived != null)
                    Debug.Log("LastAttackReceived: " + lastAttackReceived.name);
                if (lastAttackReceived == null || currentAttackReceived.name != lastAttackReceived.name)
                {
                    if (currentAttackReceived.name != "RedSpecialAttack")
                    {
                        playerScript.SpawnHitParticles(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
                    }
                    if (currentAttackReceived.name == "H1" || currentAttackReceived.name == "H2" || currentAttackReceived.name == "H3")
                    {
                        playerScript.HeavyAttackEffect();
                    }
                    Debug.Log("DAMAGED!!! By " + currentAttackReceived.name);
                    TakeDamage(currentAttackReceived.damage);
                }
                lastAttackReceived = currentAttackReceived;
            }
        }
    }


    protected override void OnDamageTaken()
    {
        anim.SetTrigger("damaged");
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
