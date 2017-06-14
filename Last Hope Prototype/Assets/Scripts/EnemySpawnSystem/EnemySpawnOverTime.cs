//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Assertions;

//public class EnemySpawnOverTime : MonoBehaviour
//{
//    public GameObject reusableSpawnPointsParent;
//    public List<float> delayBeforeWave;
//    public List<int> trashEnemiesPerWave;
//    public List<int> meleeEnemiesPerWave;
//    public List<int> rangedEnemiesPerWave;

//    public EnemySpawnManager manager;
//    public ArtilleryController artillery;

//    private List<EnemySpawnPoint> reusableSpawnPoints;
//    private bool spawning = false;
//    private bool waveSpawned = false;
//    private int currentWave = 0;

//    void Start()
//    {
//        reusableSpawnPoints = new List<EnemySpawnPoint>();

//        // Check wave count for all lists involved
//        Assert.IsTrue(trashEnemiesPerWave.Count == delayBeforeWave.Count);
//        Assert.IsTrue(trashEnemiesPerWave.Count == meleeEnemiesPerWave.Count);
//        Assert.IsTrue(trashEnemiesPerWave.Count == rangedEnemiesPerWave.Count);

//        // Find all spawnpoints to use
//        foreach (Transform child in reusableSpawnPointsParent.transform)
//        {
//            reusableSpawnPoints.Add(child.GetComponent<EnemySpawnPoint>());
//        }

//        // Check spawnpoints available
//        int trashSpawnPointsCount = 0;
//        int meleeSpawnPointsCount = 0;
//        int rangedSpawnPointsCount = 0;
//        for (int i = 0; i < reusableSpawnPoints.Count; ++i)
//        {
//            switch (reusableSpawnPoints[i].type)
//            {
//                case EnemyType.ET_TRASH:
//                    ++trashSpawnPointsCount;
//                    break;
//                case EnemyType.ET_MELEE:
//                    ++meleeSpawnPointsCount;
//                    break;
//                case EnemyType.ET_RANGED:
//                    ++rangedSpawnPointsCount;
//                    break;
//            }
//        }

//        List<int> copyList;
//        copyList = trashEnemiesPerWave;
//        copyList.Sort();
//        int maxTrashs = copyList[copyList.Count - 1];

//        copyList = meleeEnemiesPerWave;
//        copyList.Sort();
//        int maxMelees = copyList[copyList.Count - 1];

//        copyList = rangedEnemiesPerWave;
//        copyList.Sort();
//        int maxRangeds = copyList[copyList.Count - 1];

//        Assert.IsTrue(trashSpawnPointsCount >= maxTrashs);
//        Assert.IsTrue(meleeSpawnPointsCount >= maxMelees);
//        Assert.IsTrue(rangedSpawnPointsCount >= maxRangeds);
//    }

//    void Update()
//    {
//        if (spawning)
//        {
//            int waveCopy = currentWave;
//            // Check for last wave
//            if (delayBeforeWave[delayBeforeWave.Count - 1] <= 0)
//            {
//                currentWave = delayBeforeWave.Count - 1;
//            }
//            else
//            {
//                // Find current wave
//                for (int i = 0; i < delayBeforeWave.Count; ++i)
//                {
//                    if (delayBeforeWave[i] > 0)
//                    {
//                        currentWave = i;
//                        break;
//                    }
//                }
//            }
//            // Check if we're in the next wave
//            if (currentWave != waveCopy)
//            {
//                waveSpawned = false;
//            }

//            delayBeforeWave[currentWave] -= Time.deltaTime;

//            if (delayBeforeWave[currentWave] <= 0 && !waveSpawned)
//            {
//                int countTrash = trashEnemiesPerWave[currentWave];
//                int countMelee = meleeEnemiesPerWave[currentWave];
//                int countRanged = rangedEnemiesPerWave[currentWave];
//                manager.SpawnWave(reusableSpawnPoints, countTrash, countMelee, countRanged);
//                waveSpawned = true;
//            }
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        spawning = true;
//    }
//}
