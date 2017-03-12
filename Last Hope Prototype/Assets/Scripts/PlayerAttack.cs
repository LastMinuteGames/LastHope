using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeBeweenAttacks = 0.15f;
    public float timeToCombo = 0.1f;
    public GameObject attackBox;
    public GameObject swordObject;

    private PlayerSword sword;
    private float timer = 0f;
    private bool attacking;

    public enum playerState
    {
        NOT_ATTACKING = 1,
        FIRST_ATTACK = 2,
        FINAL_ATTACK = 3
    };

    public playerState state = playerState.NOT_ATTACKING;

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
            if (state == playerState.FINAL_ATTACK)
            {
                sword.EndSecondAttack();
                attacking = false;
                attackBox.SetActive(false);
            }
            else if (state == playerState.FIRST_ATTACK)
            {
                sword.EndAttack();
                attacking = false;
                attackBox.SetActive(false);
            }
            state = playerState.NOT_ATTACKING;
        }

        if (Input.GetButton("Fire1") && timer <= timeBeweenAttacks && timer >= timeToCombo && attacking && state == playerState.FIRST_ATTACK)
        {
            timer = 0;
            state = playerState.FINAL_ATTACK;
            sword.SecondAttack();
        }

        if (Input.GetButton("Fire1") && timer >= timeBeweenAttacks && state == playerState.NOT_ATTACKING)
        {
            Attack();
            state = playerState.FIRST_ATTACK;
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
