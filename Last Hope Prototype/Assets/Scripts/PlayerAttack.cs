using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeBeweenAttacks = 0.5f;
    public GameObject attackBox;
    public GameObject swordObject;

    private PlayerSword sword;
    private float timer = 0f;
    private bool attacking;

    void Awake()
    {
        attacking = false;
        attackBox.SetActive(false);
        sword = swordObject.GetComponent<PlayerSword>();
        sword.damage = 20;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (attacking && timer >= timeBeweenAttacks)
        {
            attacking = false;
            attackBox.SetActive(false);
            sword.EndAttack();
        }

        if (Input.GetButton("Fire1") && timer >= timeBeweenAttacks)
        {
            Attack();
            sword.Attack();
        }
    }

    void Attack()
    {
        timer = 0;
        attacking = true;
        attackBox.SetActive(true);
    }

}
