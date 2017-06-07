using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryEventTrigger : MonoBehaviour {
    private ArtilleryController artillery;
    public GameObject blockExit1;
    public GameObject blockExit2;

	void Start () {
        artillery = transform.GetComponentInParent<ArtilleryController>();
        blockExit1.SetActive(false);
        blockExit2.SetActive(false);
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (artillery.alive == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                artillery.alive = true;
                Debug.Log("Artillery event started");
                blockExit1.SetActive(true);
                blockExit2.SetActive(true);
            }
        }
    }
}
