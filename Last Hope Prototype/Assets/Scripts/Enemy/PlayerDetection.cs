using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    EnemyTrash enemyTrash;
    public ArtilleryController artillery;

    // Use this for initialization
    void Start()
    {
        enemyTrash = transform.gameObject.GetComponentInParent<EnemyTrash>();
        if (enemyTrash.anim != null)
        {
            switch (enemyTrash.behaviour)
            {
                case EnemyBehaviour.EB_DEFAULT:
                    if (enemyTrash.nav != null)
                    {
                        enemyTrash.nav.Stop();
                    }
                    enemyTrash.anim.SetBool("iddle", true);
                    enemyTrash.anim.SetBool("chase", false);
                    break;
                case EnemyBehaviour.EB_ARTILLERY:
                    artillery = GameObject.FindGameObjectWithTag("EventTarget").GetComponent<ArtilleryController>();
                    if (artillery != null)
                    {
                        enemyTrash.ChangeTarget(artillery.transform, TargetType.TT_ARTILLERY);
                        enemyTrash.anim.SetBool("chase", true);
                        enemyTrash.anim.SetBool("iddle", false);
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            enemyTrash.ChangeTarget(other.transform, TargetType.TT_PLAYER);
            enemyTrash.anim.SetBool("iddle", false);
            enemyTrash.anim.SetBool("chase", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch (enemyTrash.behaviour)
            {
                case EnemyBehaviour.EB_DEFAULT:
                    if (enemyTrash.nav != null)
                    {
                        enemyTrash.nav.Stop();
                    }
                    enemyTrash.anim.SetBool("iddle", true);
                    enemyTrash.anim.SetBool("chase", false);
                    break;
                case EnemyBehaviour.EB_ARTILLERY:
                    if (artillery != null)
                    {
                        enemyTrash.ChangeTarget(artillery.transform, TargetType.TT_ARTILLERY);
                        enemyTrash.anim.SetBool("chase", true);
                        enemyTrash.anim.SetBool("iddle", false);
                    }
                    break;
            }
        }
    }
}
