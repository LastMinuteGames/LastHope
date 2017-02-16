using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBeweenAttacks = 0.5f;
    GameObject player;
    PlayerHealth playerHealth;
    bool playerInRange;
    float timer = 0f;
    public int damageAmount = 10;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInRange = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (playerInRange && playerHealth.currenthp > 0 && timer > timeBeweenAttacks)
            Attack();
    }

    void Attack()
    {
        playerHealth.TakeDmg(damageAmount);
        timer = 0f;
    }
}