using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Other Layer: " + other.gameObject.layer + " Player index: " + LayerMask.NameToLayer("Player"));
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            gameObject.transform.parent.GetComponent<EnemyTrash>().OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Other Layer: " + other.gameObject.layer + " Player index: " + LayerMask.NameToLayer("Player"));
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            gameObject.transform.parent.GetComponent<EnemyTrash>().OnTriggerExit(other);
    }
}
