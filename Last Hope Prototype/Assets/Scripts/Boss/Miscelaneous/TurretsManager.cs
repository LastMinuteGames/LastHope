using UnityEngine;

public class TurretsManager : MonoBehaviour {


	public TurretController[] turrets;

	public void RestartBossCombat()
	{
		for (int i = 0; i < turrets.Length; i++)
		{
			turrets[i].Restart();
		}
	}
}
