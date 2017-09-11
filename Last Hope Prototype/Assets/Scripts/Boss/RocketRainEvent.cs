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

    public override void StartEvent()
    {
        base.StartEvent();
        Debug.Log("starting RocketRainEvent");
        manager = GameObject.Find("RocketSpawnManager").GetComponent<RocketSpawnManager>();
        if (manager == null) TerminateEvent();
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            spawnPoints[i].done = false;
            spawnPoints[i].delay = spawnPoints[i].initialDelay;
            spawnPoints[i].incoming = false;
        }
    }

    public override bool UpdateEvent()
    {
        //Debug.Log("update RocketRainEvent");
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            if(!spawnPoints[i].incoming && spawnPoints[i].delay <= 4)
            {
                GameObject incoming = Instantiate(rocketIncoming, new Vector3(spawnPoints[i].transform.position.x,
                    spawnPoints[i].transform.position.y, spawnPoints[i].transform.position.z), Quaternion.identity);
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
        base.TerminateEvent();
        Debug.Log("terminating RocketRainEvent");
    }
}
