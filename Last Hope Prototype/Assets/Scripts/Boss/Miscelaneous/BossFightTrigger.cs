using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour {


    private BossManager bossManager;


    void Start ()
    {
        bossManager = GameObject.FindGameObjectWithTag("Boss").GetComponentInParent<BossManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("you came to the boss zone!");

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (bossManager)
            {
                bossManager.StartBossFight();
                gameObject.SetActive(false);
            }
        }
    }






}
