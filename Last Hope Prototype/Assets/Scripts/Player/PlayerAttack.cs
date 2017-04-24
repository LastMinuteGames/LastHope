using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeBeweenAttacks = 0.15f;
    public float timeToCombo = 0.1f;

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
                attacking = false;
            }
            else if (state == playerState.FIRST_ATTACK)
            {
                attacking = false;
            }
            state = playerState.NOT_ATTACKING;
        }

        if (InputManager.LightAttack() && timer <= timeBeweenAttacks && timer >= timeToCombo && attacking && state == playerState.FIRST_ATTACK)
        {
            timer = 0;
            state = playerState.FINAL_ATTACK;
        }

        if (InputManager.LightAttack() && timer >= timeBeweenAttacks && state == playerState.NOT_ATTACKING)
        {
            Attack();
            state = playerState.FIRST_ATTACK;
        }
    }

    void Attack()
    {
        timer = 0;
        attacking = true;
    }

}
