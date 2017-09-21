using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfElevator : MonoBehaviour {

    public BoxCollider triggerCollider;
    public BoxCollider blockCollider;

    private bool closed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!closed && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            closed = true;
            blockCollider.enabled = true;
        }
    }
}
