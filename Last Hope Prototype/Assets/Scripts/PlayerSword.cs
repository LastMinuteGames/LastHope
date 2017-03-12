using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{

    bool isAttacking = false;
    public int damage = 10;
    
    void Start()
    {

    }
    
    void Update()
    {

    }

    public void Attack()
    {
        isAttacking = true;
        this.transform.Rotate(new Vector3(90, 0, 0));
    }

    public void EndAttack()
    {
        isAttacking = false;
        this.transform.Rotate(new Vector3(-90, 0, 0));
    }

    public void SecondAttack()
    {
        this.transform.Rotate(new Vector3(0, -90, 0));
    }

    public void EndSecondAttack()
    {
        this.transform.Rotate(new Vector3(0, 90, 0));
        this.transform.Rotate(new Vector3(-90, 0, 0));
    }
}
