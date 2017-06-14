using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.EnemySpawnSystem
{
    public class Spawn
    {
        public EnemyType type;
        public uint number;

        public Spawn(EnemyType type, uint number)
        {
            this.type = type;
            this.number = number;
        }
    }
}
