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
            anim.SetTrigger("Break");
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (layersToCollideWith == (layersToCollideWith | (1 << other.gameObject.layer)) && other.tag == "RedAttack")
        {
            this.GetComponent<BoxCollider>().isTrigger = true;
            anim.SetTrigger("Break");
            //broke = true;
            //Destroy(this.gameObject);
        }
    }

    void Break()
    {
        Destroy(this.gameObject);
    }
}
