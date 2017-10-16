using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocketController : MonoBehaviour {

	public GameObject incomingParticleGO;
	public GameObject rocketGO;
	public float anticipationTime = 2f;

	private float timer;

	void Start()
	{
		incomingParticleGO.SetActive (true);
		timer = 0;
	}

	void Update () 
	{
		timer += Time.deltaTime;

		if (timer > anticipationTime) {
			SpawnRocket ();
		}
	}

	void SpawnRocket()
	{
		incomingParticleGO.SetActive (false);
		Instantiate (rocketGO, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
