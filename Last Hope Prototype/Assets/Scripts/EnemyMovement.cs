using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;
    PlayerHealth playerhp;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerhp = player.GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (playerhp.currenthp > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }


}
