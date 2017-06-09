using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryController : MonoBehaviour
{

    public float maxHp = 100;
    public float currentHp;
    public bool alive = true;
    public ParticleSystem leftBarrelParticles;
    public ParticleSystem rightBarrelParticles;
    
    void Start()
    {
        currentHp = maxHp;
    }
    
    void Update()
    {
        if (alive)
        {
            if (currentHp <= 0)
            {
                Die();
            }
            else
            {
                // TODO: Wait for all enemies to be killed to finish the event. Unlock main square doors to proceed
                Debug.Log("You win");
            }
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack") && alive)
        {
            int damage = 10;
            TakeDamage(damage);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
    void Die()
    {
        alive = false;
        Debug.Log("You lose");
        Destroy(this.gameObject);
    }

    void LeftBarrelShoot()
    {
        if (leftBarrelParticles != null)
        {
            leftBarrelParticles.Play();
        }
    }
    void RightBarrelShoot()
    {
        if (rightBarrelParticles != null)
        {
            rightBarrelParticles.Play();
        }
    }
}
