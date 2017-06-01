using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    EnemyTrash enemyTrash;

    // Use this for initialization
    void Start()
    {
        enemyTrash = transform.gameObject.GetComponentInParent<EnemyTrash>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemyTrash.ChangeTarget(other.transform);
            enemyTrash.anim.SetBool("iddle", false);
            enemyTrash.anim.SetBool("chase", true);
        } else
        {
            if (enemyTrash.nav != null)
            {
                enemyTrash.nav.Stop();
            }
            enemyTrash.anim.SetBool("iddle", true);
            enemyTrash.anim.SetBool("chase", false);
        }
    }
}
