using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/MortarAttackEvent")]
public class MortarAttackEvent : BossEvent 
{
	public float rocketAmount;
	public GameObject bossRocketPrefab;

	public Transform spawnPointsCenter;
	public float marginX = 105;
	public float marginZ = 15;
	public float minX, maxX, minZ, maxZ;

	public override void StartEvent()
	{
		base.StartEvent ();
		Debug.Log("starting MortarEvent");
		BossManager.instance.SetEmisiveYellow();
		GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().SetTrigger("mortarAttack");

		CalculateMargins ();
		SpawnRockets ();
	}

	public override bool UpdateEvent()
	{
		//Debug.Log("update RocketRainEvent");
		return base.UpdateEvent();
	}

	public override void TerminateEvent()
	{
		base.TerminateEvent();
		Debug.Log("terminating MortarEvent");
	}

	void CalculateMargins()
	{
		minX = spawnPointsCenter.position.x - marginX;
		maxX = spawnPointsCenter.position.x + marginX;
		minZ = spawnPointsCenter.position.z - marginZ;
		maxZ = spawnPointsCenter.position.z + marginZ;
	}

	void SpawnRockets()
	{
		for (int i = 0; i < rocketAmount; ++i) 
		{
			float x = Random.Range (minX, maxX);
			float y = spawnPointsCenter.position.y;
			float z = Random.Range (minZ, maxZ);

			Vector3 targetPosition = new Vector3 (x, y, z);
			Instantiate (bossRocketPrefab, targetPosition, Quaternion.identity);
		}
	}

}
