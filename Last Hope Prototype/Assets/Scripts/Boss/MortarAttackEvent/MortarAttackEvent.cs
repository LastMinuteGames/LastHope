using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/MortarAttackEvent")]
public class MortarAttackEvent : BossEvent 
{
	public int rocketAmount;
	public float timeBetweenRockets = 0.2f;
	public GameObject bossRocketPrefab;
	public Transform spawnPointsCenter;
	public float marginX = 105;
	public float marginZ = 15;

	private float minX, maxX, minZ, maxZ;
	private bool started = false;
	private float timer = 0;
	private int spawnedAmount;

	public override void StartEvent()
	{
		base.StartEvent ();
		Debug.Log("starting MortarEvent");
		started = false;
		timer = 0;
		spawnedAmount = 0;
		BossManager.instance.SetEmisiveYellow();
		GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().SetTrigger("mortarAttack");

		CalculateMargins ();
	}

	public override bool UpdateEvent()
	{
		if (ellapsedTime >= anticipationTime && started == false) {
			started = true;
		}

		if (!started) {
			return base.UpdateEvent();
		}

		timer += Time.deltaTime;
		if (timer >= timeBetweenRockets) {
			timer -= timeBetweenRockets;
			SpawnRocket();
		}

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

	void SpawnRocket()
	{
		if (spawnedAmount >= rocketAmount) {
			return;
		}

		float x = Random.Range (minX, maxX);
		float y = spawnPointsCenter.position.y;
		float z = Random.Range (minZ, maxZ);

		Vector3 targetPosition = new Vector3 (x, y, z);
		Instantiate (bossRocketPrefab, targetPosition, Quaternion.identity);

		spawnedAmount++;
	}

}
