//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AttackInRange : MonoBehaviour {
//    public int timeBetweenAttacks;
//    float startTime = 0;
//    private EnemyTrash enemyTrash;
//    private bool playerInRange;

//	// Use this for initialization
//	void Start () {
//        enemyTrash = gameObject.transform.parent.GetComponent<EnemyTrash>();
//        playerInRange = false;
//    }
	
//	// Update is called once per frame
//	void Update () {
//        if(playerInRange == true)
//        {
//            startTime += Time.deltaTime;
//            if (startTime >= timeBetweenAttacks)
//            {
//                startTime = 0;
//            }
//        }
//	}

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
//        {
//            enemyTrash.OnPlayerInRange(other);
//            startTime = 0;
//            playerInRange = true;
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
//        {
//            playerInRange = false;
//            startTime = 0;
//        }
//    }
//}
