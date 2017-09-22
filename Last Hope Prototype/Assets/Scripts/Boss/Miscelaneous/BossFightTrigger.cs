using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("you came to the boss zone!");

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            BossManager.instance.StartBossFight();
            gameObject.SetActive(false);
        }
    }
}
