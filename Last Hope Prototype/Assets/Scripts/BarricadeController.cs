using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour {

    public LayerMask layersToCollideWith;
    public bool broke = false;
    [HideInInspector]
    public Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (broke)
        {
            anim.SetBool("break", true);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Aux");
        Debug.Log(other.tag);
        if (layersToCollideWith == (layersToCollideWith | (1 << other.gameObject.layer)) && other.tag == "RedAttack")
        {
            Debug.Log("Aux!");
            this.GetComponent<BoxCollider>().isTrigger = true;
            anim.SetBool("break", true);
            //Destroy(this.gameObject);
        }
    }

    void Break()
    {
        Debug.Log("PADRE! Lo del break!");
        Destroy(this.gameObject);
    }

    /*void OnCollisionEnter(Collision col)
    {
        Debug.Log("Colision con:");
        Debug.Log(col.gameObject.tag);
        if (layersToCollideWith == (layersToCollideWith | (1 << col.gameObject.layer)) && col.gameObject.tag == "NeutralAttack")
        {
            Debug.Log("Aux!");
            Destroy(this.gameObject);
        }
    }*/
}
