using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleEnviroment : MonoBehaviour {

    public GameObject deadExplosion;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
			if (this.gameObject.name.Contains ("StreetLamp") || this.gameObject.name.Contains ("TrafficLight") || this.gameObject.name.Contains ("SquareLamp"))
			{
				AudioSources.instance.PlaySound ((int)AudiosSoundFX.Environment_BreakEnvironment_BreakTrafficLight);
			} 
			else if (this.gameObject.name.Contains ("Bench")) 
			{
				AudioSources.instance.PlaySound ((int)AudiosSoundFX.Environment_BreakEnvironment_BreakBench);
			}
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
