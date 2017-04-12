using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    ET_TRASH,
    ET_MELEE,
    ET_RANGED
}

public class EnemySpawnManager : MonoBehaviour {
    public GameObject trashEnemy;
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;

    private List<GameObject> enemies;

    public GameObject SpawnEnemy(Transform transform, EnemyType type)
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
        GameObject spawnedEnemy = Instantiate(originalEnemy, transform.position, transform.rotation);
        //enemies.Add(spawnedEnemy);
        return spawnedEnemy;
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
