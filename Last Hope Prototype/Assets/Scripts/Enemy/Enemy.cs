using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    ET_TRASH,
    ET_MELEE,
    ET_RANGED
}

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


public class Enemy : MonoBehaviour
{
    protected bool autokill = true;
    [SerializeField]
    protected int life;
    [SerializeField]
    protected int maxLife;

    [SerializeField]
    protected GameObject lifeDrop;
    [SerializeField]
    protected int lifeDropProbability;
    [SerializeField]
    protected GameObject EnergyDrop;
    [SerializeField]
    protected int energyDropProbability;

    [HideInInspector]
    public EnemyType enemyType;
    [HideInInspector]
    public EnemyBehaviour behaviour;
    protected Target target;

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

    }

    public bool Autokill
    {
        get
        {
            return autokill;
        }

        set
        {
            autokill = value;
        }
    }

    public void RecoveryHealth(int quantity)
    {
        life += quantity;
        if (life > maxLife)
            life = maxLife;
    }

    public void Dead()
    {
        if (autokill == true)
        {
            float lifeRandomNumber = UnityEngine.Random.Range(0, 100.0f);
            float energyRandomNumber = UnityEngine.Random.Range(0, 100.0f);

            if (lifeRandomNumber < lifeDropProbability)
            {
                Instantiate(lifeDrop, transform.position, Quaternion.identity);
            }

            if (energyRandomNumber < energyDropProbability)
            {
                Instantiate(EnergyDrop, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        else
        {
            autokill = true;
        }

    }

    public bool IsDead()
    {
        return life <= 0;
    }

    public void TakeDamage(int damage)
    {
        if (life > 0)
        {
            life -= damage;
            OnDamageTaken();
        }
    }

    virtual protected void OnDamageTaken()
    {

    }
}
