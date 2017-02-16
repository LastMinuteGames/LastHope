using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeBeweenAttacks = 0.5f;
    public GameObject attackBox;

    private float timer = 0f;
    private bool attacking;

    void Awake()
    {
        attacking = false;
        attackBox.SetActive(false);
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
        }

        if (Input.GetButton("Fire1") && timer >= timeBeweenAttacks)
        {
            Attack();
        }
    }

    void Attack()
    {
        timer = 0;
        attacking = true;
        attackBox.SetActive(true);
    }

}
