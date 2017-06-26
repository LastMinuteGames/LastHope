using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleEnviroment : MonoBehaviour {

    public GameObject deadExplosion;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            SpawnExplosion();
            Destroy(transform.parent.gameObject);
        }
    }

    void SpawnExplosion()
    {
        GameObject particle = Instantiate(deadExplosion, transform.position, transform.rotation);
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        float totalDuration = ps.main.duration + ps.main.startLifetime.constantMax;
        Destroy(particle, totalDuration);
    }
}
