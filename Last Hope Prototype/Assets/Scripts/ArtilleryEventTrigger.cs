using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryEventTrigger : MonoBehaviour {
    private ArtilleryController artillery;

	void Start () {
        artillery = transform.GetComponentInParent<ArtilleryController>();
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (artillery.alive == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                artillery.alive = true;
                Debug.Log("Artillery event started");
            }
        }
    }
}
