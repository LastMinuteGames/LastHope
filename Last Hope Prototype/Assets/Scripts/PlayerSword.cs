using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour {

    bool isAttacking = false;
    public int damage = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack()
    {
        isAttacking = true;
        this.transform.Rotate(new Vector3(90,0,0));
    }

    public void EndAttack()
    {
        isAttacking = false;
        this.transform.Rotate(new Vector3(-90, 0, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isAttacking)
        {
            Debug.Log("Aux!");
            Destroy(other.gameObject);
        }
    }
}
