﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryEventTrigger : MonoBehaviour {
    private ArtilleryController artillery;
    public GameObject blockExit1;
    public GameObject blockExit2;

	void Start () {
        artillery = transform.GetComponentInParent<ArtilleryController>();
        if (blockExit1 != null && blockExit2 != null)
        {
            blockExit1.SetActive(false);
            blockExit2.SetActive(false);
        }
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (artillery.alive == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                artillery.alive = true;
                Debug.Log("Artillery event started");
                BlockExits();
            }
        }
    }

    public void BlockExits()
    {
        if (blockExit1 != null && blockExit2 != null)
        {
            blockExit1.SetActive(true);
            blockExit2.SetActive(true);
        }
    }

    public void UnblockExits()
    {
        if (blockExit1 != null && blockExit2 != null)
        {
            Destroy(blockExit1);
            Destroy(blockExit2);
        }
    }
}
