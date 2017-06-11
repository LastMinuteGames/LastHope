using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Assertions;

namespace Assets.Scripts.EnemySpawnSystem
{
    [System.Serializable]
    public class Wave
    {
        public Dictionary<EnemyType, uint> currentEnemies; //current enemies spawned
        private Dictionary<EnemyType, uint> totalEnemiesAtTime; //max number of enemies by type
        private Dictionary<EnemyType, uint> totalEnemies; //number of enemies by type in total for this wave
        private bool started = false;


        public Wave()
        {
            currentEnemies = new Dictionary<EnemyType, uint>();
            totalEnemiesAtTime = new Dictionary<EnemyType, uint>();
            totalEnemies = new Dictionary<EnemyType, uint>();
        }

        public void AddEnemy(EnemyType type, uint numberOfTotalEnemiesAtTime, uint numberOfTotalEnemies)
        {
            Assert.IsTrue(numberOfTotalEnemiesAtTime <= numberOfTotalEnemies && numberOfTotalEnemies > 0 && numberOfTotalEnemiesAtTime > 0);
            totalEnemies[type] = numberOfTotalEnemies;
            totalEnemiesAtTime[type] = numberOfTotalEnemiesAtTime;
            currentEnemies[type] = 0;
        }

        public List<Spawn> StartWave()
        {
            List<Spawn> result = new List<Spawn>();
            if(started == false)
            {
                started = true;
                foreach(EnemyType type in totalEnemiesAtTime.Keys)
                {
                    uint number = totalEnemiesAtTime[type];
                    totalEnemies[type] -= number;
                    currentEnemies[type] += number;
                    result.Add(new Spawn(type, number));
                }
            }


            return result;
        }

        public Spawn RemoveEnemies(EnemyType type, uint number = 1)
        {
            Spawn result = null;
            if (started == true && currentEnemies.ContainsKey(type))
            {
                Assert.IsTrue(currentEnemies[type] > 0);
                currentEnemies[type] -= number;
                uint maxNumberOfPossibleEnemiesToSpawn = totalEnemiesAtTime[type] - currentEnemies[type];
                if (maxNumberOfPossibleEnemiesToSpawn > 0 && totalEnemies[type] >= maxNumberOfPossibleEnemiesToSpawn)
                {
                    uint resultToSpawn = Math.Min(maxNumberOfPossibleEnemiesToSpawn, totalEnemies[type]);
                    result = new Spawn(type, resultToSpawn);

                    totalEnemies[type] -= resultToSpawn;
                }

            }

            return result;
        }

        public List<Spawn> RemoveEnemies(Dictionary<EnemyType, uint> enemies)
        {
            List<Spawn> result = new List<Spawn>();
            if(enemies.Count > 0)
            {
                foreach(EnemyType type in enemies.Keys)
                {
                    Spawn spawn = RemoveEnemies(type, enemies[type]);
                    if(spawn != null)
                    {
                        result.Add(spawn);
                    }
                }
            }

            return result;
        }

        public bool IsFinished()
        {
            bool result = true;
            foreach (EnemyType type in totalEnemiesAtTime.Keys)
            {
                if(currentEnemies[type] != 0 && totalEnemies[type] != 0)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }


    }
}
