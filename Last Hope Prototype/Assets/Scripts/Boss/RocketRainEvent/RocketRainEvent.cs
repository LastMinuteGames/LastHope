using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss/Events/RocketRainEvent")]
public class RocketRainEvent : BossEvent
{
    public List<RocketSpawnPoint> spawnPoints;
    private RocketSpawnManager manager;
    public GameObject rocketIncoming;
    private List<GameObject> incomings;
	private bool started = false;


    public override void StartEvent()
    {
		started = false;
        base.StartEvent();
        //BossManager.instance.SetEmisiveMortar();
        Debug.Log("starting RocketRainEvent");
        manager = GameObject.Find("RocketSpawnManager").GetComponent<RocketSpawnManager>();

		if (manager == null) {
			TerminateEvent ();
		}

        RandomizeSpawnPoints();
        InitializeSpawnPoints();
		BossManager.instance.SetEmisiveYellow();
		GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().SetTrigger("mortarAttack");
    }

    public override bool UpdateEvent()
    {

        if (ellapsedTime >= anticipationTime && started == false)
        {
            started = true;
        }

        if (!started)
        {
            return base.UpdateEvent();
        }


        //Debug.Log("update RocketRainEvent");
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            if (!spawnPoints[i].incoming && spawnPoints[i].delay <= 4)
            {
                int num = (int)Math.Round(UnityEngine.Random.Range(-30.0f, 30.0f));
                //Vector3 instantiatePos = spawnPoints [i].transform.position;
                Quaternion instantiateRot = Quaternion.Euler(-90, 0, 0);
                Vector3 pos = spawnPoints[i].transform.position;
                pos.x += num;
                GameObject incoming = Instantiate(rocketIncoming, pos, instantiateRot);
                incomings.Add(incoming);
                spawnPoints[i].incoming = true;
                spawnPoints[i].incomingObject = incoming;
            }

            if (spawnPoints[i].delay <= 0 && !spawnPoints[i].done)
            {
                manager.SpawnRocket(spawnPoints[i]);
                spawnPoints[i].done = true;
                spawnPoints[i].delay = spawnPoints[i].initialDelay;
                if (spawnPoints[i].incoming)
                {
                    incomings.Remove(spawnPoints[i].incomingObject);
                    Destroy(spawnPoints[i].incomingObject);
                }
                //Destroy(spawnPoints[i]);
                //spawnPoints.RemoveAt(i);
            }
            else
            {
                spawnPoints[i].delay -= Time.deltaTime;
            }
        }

        //if (incomings.Count == 0) Debug.Log("ESTOY VACIO");

        return base.UpdateEvent();
    }

    public override void TerminateEvent()
    {
        EliminateSpawnPointsParticles();
        base.TerminateEvent();
        Debug.Log("terminating RocketRainEvent");
    }

    private void InitializeSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            spawnPoints[i].done = false;
            spawnPoints[i].delay = spawnPoints[i].initialDelay;
            spawnPoints[i].incoming = false;
        }
    }

    private void EliminateSpawnPointsParticles()
    {
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            if (spawnPoints[i].incoming)
            {
                incomings.Remove(spawnPoints[i].incomingObject);
                Destroy(spawnPoints[i].incomingObject);
            }
        }
    }

    private void RandomizeSpawnPoints()
    {
        //spawnPoints.Find(x => x.done == false);
        int[] discarted = new int[spawnPoints.Count];
        int discartedIndex = 0;
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            bool done = false;
            while (!done)
            {
                int num = (int)Math.Round(UnityEngine.Random.Range(1.0f, 16.0f));
                bool found = false;
                for (int j = 0; j < discarted.Length; j++)
                {
                    if (discarted[j] == num) found = true;
                }
                if (!found)
                {
                    spawnPoints[i].order = discartedIndex;
                    spawnPoints[i].initialDelay = 2f + 0.5f * discartedIndex;
                    discarted[discartedIndex] = num;
                    discartedIndex++;
                    done = true;
                }
            }

        }

        spawnPoints.Sort((x, y) => x.order.CompareTo(y.order));

    }
}
