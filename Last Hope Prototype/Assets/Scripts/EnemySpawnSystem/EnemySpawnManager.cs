using Assets.Scripts.EnemySpawnSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class EnemySpawnManager : MonoBehaviour
{
    public GameObject trashEnemy;
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;

    public GameObject SpawnEnemy(EnemySpawnPoint point)
    {       
        //So we don't break anything
        return SpawnEnemy(point, point.type);
    }

    public GameObject SpawnEnemy(EnemySpawnPoint point, EnemyType type)
    {
        GameObject originalEnemy;
        switch (type)
        {
            case EnemyType.ET_TRASH:
                originalEnemy = trashEnemy;
                break;
            case EnemyType.ET_MELEE:
                originalEnemy = meleeEnemy;
                break;
            case EnemyType.ET_RANGED:
                originalEnemy = rangedEnemy;
                break;
            default:
                originalEnemy = trashEnemy;
                break;
        }

        GameObject spawnedEnemy = Instantiate(originalEnemy, point.transform.position, point.transform.rotation);
        return spawnedEnemy;
    }

    public GameObject SpawnEnemy(EnemySpawnPoint point, EnemyType type, EnemyObserver enemyObserver)
    {
        GameObject go = SpawnEnemy(point, type);
        CapsuleController capsuleController = go.GetComponent<CapsuleController>();
        if(capsuleController != null && enemyObserver != null)
        {
            capsuleController.Observer = enemyObserver;
        }

        return go;
    }

    //public List<GameObject> SpawnWave(List<EnemySpawnPoint> points, int countTrash, int countMelee, int countRanged)
    //{
    //    List<GameObject> spawnedWave = new List<GameObject>();
    //    GameObject originalEnemy = null;
    //    for (int i = 0; i < points.Count; i++)
    //    {
    //        switch (points[i].type)
    //        {
    //            case EnemyType.ET_TRASH:
    //                if (countTrash > 0)
    //                {
    //                    originalEnemy = trashEnemy;
    //                    countTrash--;
    //                }
    //                break;
    //            case EnemyType.ET_MELEE:
    //                if (countMelee > 0)
    //                {
    //                    originalEnemy = meleeEnemy;
    //                    countTrash--;
    //                }
    //                break;
    //            case EnemyType.ET_RANGED:
    //                if (countRanged > 0)
    //                {
    //                    originalEnemy = rangedEnemy;
    //                    countTrash--;
    //                }
    //                break;
    //            default:
    //                if (countTrash > 0)
    //                {
    //                    originalEnemy = trashEnemy;
    //                    countTrash--;
    //                }
    //                break;
    //        }
    //        if (originalEnemy != null)
    //        {
    //            GameObject spawnedEnemy = Instantiate(originalEnemy, points[i].transform.position, points[i].transform.rotation);
    //            spawnedWave.Add(spawnedEnemy);
    //        }
    //        if (countTrash <= 0 && countMelee <= 0 && countRanged <= 0)
    //        {
    //            break;
    //        }
    //    }
    //    return spawnedWave;
    //}
}
