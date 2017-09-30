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

        Debug.Log(enemies.Count);
    }

    void DeleteEnemy(EnemyTrash enemy)
    {
        
        if(enemies.Count == 1)
        {
            
            isInRound = false;
            EndRound(round);
            Debug.Log("Muertos todos!!");
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
            turrets[num - 1].ChangeEmissives(true);
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
