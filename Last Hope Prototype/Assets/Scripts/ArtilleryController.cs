using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArtilleryController : MonoBehaviour
{

    public float maxHp = 100;
    public float currentHp;
    [HideInInspector]
    public bool alive = true;
    public ParticleSystem leftBarrelParticles;
    public ParticleSystem rightBarrelParticles;
    public GameObject deadExplosion;
    public GameObject deadDecal;
    public Slider hpSlider;

    private ArtilleryEventTrigger eventTrigger;

    void Start()
    {
        currentHp = maxHp;
        hpSlider.maxValue = maxHp;
        UpdateHpBar();
        eventTrigger = GetComponentInChildren<ArtilleryEventTrigger>();
        alive = true;
    }

    //void Update()
    //{
    //    if (alive)
    //    {
    //        if (currentHp <= 0)
    //        {
    //            Die();
    //        }
    //        else
    //        {
    //            // TODO: Wait for all enemies to be killed to finish the event. Unlock main square doors to proceed
    //            Debug.Log("You win");
    //            //eventTrigger.UnblockExits();
    //        }
    //    }
    //}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack") && alive)
        {
            EnemyTrash trashScript = other.gameObject.GetComponentInParent<EnemyTrash>();
            Attack currentAttackReceived = trashScript.GetAttack();
            if (currentAttackReceived != null)
            {
                TakeDamage(currentAttackReceived.damage);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        UpdateHpBar();
        if (currentHp <= 0)
        {
            SpawnExplosion();
            SpawnDecal();
            alive = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void Die()
    {
        Debug.Log("You lose");
        SpawnExplosion();
        SpawnDecal();
        alive = false;
        Destroy(gameObject);
    }

    void SpawnExplosion()
    {
        Instantiate(deadExplosion, transform.position, transform.rotation);
    }

    void SpawnDecal()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);
        Quaternion hitRotation = Quaternion.Euler(90, Random.Range(0, 360), 0);
        GameObject spawnedDecal = Instantiate(deadDecal, hit.point + new Vector3(0, 0.001f, 0), hitRotation);
        spawnedDecal.transform.localScale *= 5;
    }

    void LeftBarrelShoot()
    {
        if (leftBarrelParticles != null)
        {
            leftBarrelParticles.Play();
        }
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Shot);
    }
    void RightBarrelShoot()
    {
        if (rightBarrelParticles != null)
        {
            rightBarrelParticles.Play();
        }
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Shot);
    }

    void MovementSound()
    {
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Environment_Artillery_Movement);
    }

    void UpdateHpBar()
    {
        hpSlider.value = currentHp;
    }



}
