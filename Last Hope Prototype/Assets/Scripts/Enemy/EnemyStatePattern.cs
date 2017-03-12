using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatePattern : MonoBehaviour {
    private UnityEngine.AI.NavMeshAgent nav;
    public Transform enemy;

    

    void Awake()
    {
    }

    // Use this for initialization
    void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {

    }
}
