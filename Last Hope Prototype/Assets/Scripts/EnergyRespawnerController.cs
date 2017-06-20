using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRespawnerController : MonoBehaviour
{
    [SerializeField]
    GameObject item;
    GameObject spawnedItem;
    [SerializeField]
    float cooldown = 10;
    float timer;
    
    void Start()
    {
        timer = 0;
        SpawnItem();
    }
    
    void Update()
    {
        if (!spawnedItem)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;
                SpawnItem();
            }
        }

    }

    void SpawnItem()
    {
        spawnedItem = Instantiate(item, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
    }
}
