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
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.EnableRedAbility();
            player.SwitchToRed();
            gameObject.SetActive(false);
            Debug.Log("Red special ability learned");
        }
    }
}
