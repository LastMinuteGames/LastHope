using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/MortarAttackEvent")]
public class MortarAttackEvent : BossEvent 
{
	public Transform[] spawnPoints;
	public GameObject bossRocketPrefab;

	public override void StartEvent()
	{
		base.StartEvent ();
		Debug.Log("starting MortarEvent");
		BossManager.instance.SetEmisiveYellow();
		GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().SetTrigger("mortarAttack");

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

	void SpawnRockets()
	{
		for (int i = 0; i < spawnPoints.Length; ++i) 
		{
			Instantiate (bossRocketPrefab, spawnPoints [i].position, Quaternion.identity);
		}
	}

}
