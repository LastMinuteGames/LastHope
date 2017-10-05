using System.Collections.Generic;
using UnityEngine;

public class TurretsManager : MonoBehaviour {


	public TurretController[] turrets;
    public List<CapsuleController> capsules;
    public List<EnemyTrash> enemies;
    private int round = 0;
    private bool isInRound = false;

    public void RestartBossCombat()
	{
		for (int i = 0; i < turrets.Length; i++)
		{
			turrets[i].Restart();
		}
        isInRound = false;
        round = 0;
        capsules.Clear();

		foreach (EnemyTrash enemy in enemies) {
			Destroy (enemy.gameObject);
		}
		enemies.Clear();
	}

    void Update()
    {
        if (capsules.Count > 0) ManageCapsules();

        if (enemies.Count > 0)
        {
            if (!isInRound)
            {
                round++;
                isInRound = true;
                StartRound(round);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                EnemyTrash aux = enemies[i];
                if (aux != null)
                { 
                    Debug.Log(aux.GetLife());
                    if (aux.GetLife() <= 0)
                    {
                        DeleteEnemy(aux);
                    }
                }
            }
        }
    }

    void DeleteEnemy(EnemyTrash enemy)
    {
        if(enemies.Count == 1)
        {
            isInRound = false;
            EndRound(round);
        }
        enemies.Remove(enemy);
    }

    void StartRound(int num)
    {

    }

    void EndRound(int num)
    {
        if (num <= 2)
        {
			turrets[num - 1].Unlock();
        }
		if (num == 1) {
			DialogueSystem.Instance.AddDialogue ("Left turret activated", "", 3.5f);
		}
		if (num == 2) {
			DialogueSystem.Instance.AddDialogue ("Right turret activated", "", 3.5f);
		}
    }

    void ManageCapsules()
    {
        for(int i=0; i < capsules.Count; i++)
        {
            GameObject aux = capsules[i].go;
            if (aux != null)
            {
                enemies.Add(aux.GetComponent<EnemyTrash>());
                capsules.Remove(capsules[i]);
            }
        }
    }
}
