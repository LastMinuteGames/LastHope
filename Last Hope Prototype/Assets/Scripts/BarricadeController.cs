using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour {

    public LayerMask layersToCollideWith;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Aux");
        Debug.Log(other.tag);
        if (layersToCollideWith == (layersToCollideWith | (1 << other.gameObject.layer)) && other.tag == "RedAttack")
        {
            Debug.Log("Aux!");
            Destroy(this.gameObject);
        }
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
