using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityController : MonoBehaviour {
    
	void Start () {
		
	}

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().EnableRedAbility();
            gameObject.SetActive(false);
        }
    }
}
