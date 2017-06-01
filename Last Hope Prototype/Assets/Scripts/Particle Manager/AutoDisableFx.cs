using UnityEngine;
using System.Collections;

public class AutoDisableFx : MonoBehaviour
{
	private ParticleSystem mParticleSystem;

	void Start()
	{
		mParticleSystem = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		if(!mParticleSystem.IsAlive(true))
		{
			gameObject.SetActive(false);
		}
	}
}
