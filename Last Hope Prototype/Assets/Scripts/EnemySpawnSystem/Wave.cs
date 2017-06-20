using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Assertions;
using UnityEngine;

namespace Assets.Scripts.EnemySpawnSystem
{
    [System.Serializable]
    public class Wave
    {
        public Dictionary<EnemyType, uint> currentEnemies; //current enemies spawned
        private Dictionary<EnemyType, uint> totalEnemiesAtTime; //max number of enemies by type
        private Dictionary<EnemyType, uint> totalEnemies; //number of enemies by type in total for this wave
        private bool started = false;
        private String name;


        public Wave(String name)
        {
            currentEnemies = new Dictionary<EnemyType, uint>();
            totalEnemiesAtTime = new Dictionary<EnemyType, uint>();
            totalEnemies = new Dictionary<EnemyType, uint>();
            this.name = name;
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
                Assert.IsTrue(currentEnemies[type] >= number);
                currentEnemies[type] -= number;
                uint maxNumberOfPossibleEnemiesToSpawn = totalEnemiesAtTime[type] - currentEnemies[type];
                uint resultToSpawn = 0;
                if (maxNumberOfPossibleEnemiesToSpawn > 0)
                {
                    resultToSpawn = Math.Min(maxNumberOfPossibleEnemiesToSpawn, totalEnemies[type]);
                    result = new Spawn(type, resultToSpawn);
                    totalEnemies[type] -= resultToSpawn;
                    currentEnemies[type] += resultToSpawn;
                }
                //Debug.Log("Number: " + number + " ResultToSpawn " + resultToSpawn + " Max: " + maxNumberOfPossibleEnemiesToSpawn + " Total: " + totalEnemies[type] + " totalAtTime: " + totalEnemiesAtTime[type] + " current: " + currentEnemies[type]);
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
                //Debug.Log("Spawns Added: " + result.Count + " Enemies Sended: " + enemies.Count);
            }

            return result;
        }

        public bool IsFinished()
        {
            bool result = true;
            foreach (EnemyType type in totalEnemiesAtTime.Keys)
            {
                if(currentEnemies[type] != 0 || totalEnemies[type] != 0)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public void FinishDebug()
        {
            //Debug.Log("Wave " + name + " has: ");
            foreach (EnemyType type in totalEnemiesAtTime.Keys)
            {
                //Debug.Log("Wave " + name + " has: " + totalEnemiesAtTime[type] + " enemies at time " + totalEnemies[type] + " remaining enemies " + currentEnemies[type] + " current enemies");
            }
        }

    }
}
