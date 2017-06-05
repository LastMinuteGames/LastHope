using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Player
{
    public enum PlayerStanceType
    {
        STANCE_NONE,
        STANCE_BLUE,
        STANCE_RED,
        STANCE_UNDEFINED
    }
    [System.Serializable]
    public class PlayerPassiveStats
    {
        //public float attackSpeed;
        public float attackDamage;
        public float blockingMovementSpeed;
        public float movementSpeed;
        public float specialAttackDamage;

        public PlayerPassiveStats(float attackDamage, float blockingMovementSpeed, float movementSpeed, float specialAttackDamage)
        {
            this.attackDamage = attackDamage;
            this.blockingMovementSpeed = blockingMovementSpeed;
            this.movementSpeed = movementSpeed;
            this.specialAttackDamage = specialAttackDamage;
        }
    }

    [System.Serializable]
    public class PlayerPassiveStatsRelative : PlayerPassiveStats
    {
        public PlayerPassiveStatsRelative(float attackDamage, float blockingMovementSpeed, float movementSpeed, float specialAttackDamage) : base(attackDamage, blockingMovementSpeed, movementSpeed, specialAttackDamage)
        {
        }
        public static PlayerPassiveStatsAbsolute operator *(PlayerPassiveStatsRelative one, PlayerPassiveStatsAbsolute other)
        {
            PlayerPassiveStatsAbsolute result = new PlayerPassiveStatsAbsolute(one.attackDamage * other.attackDamage, one.blockingMovementSpeed * other.blockingMovementSpeed,
                one.movementSpeed * other.movementSpeed, one.specialAttackDamage * other.specialAttackDamage);

            return result;
        }

        public static PlayerPassiveStatsAbsolute operator *(PlayerPassiveStatsAbsolute other, PlayerPassiveStatsRelative one)
        {
            PlayerPassiveStatsAbsolute result = new PlayerPassiveStatsAbsolute(one.attackDamage * other.attackDamage, one.blockingMovementSpeed * other.blockingMovementSpeed,
                one.movementSpeed * other.movementSpeed, one.specialAttackDamage * other.specialAttackDamage);

            return result;
        }
    }

    [System.Serializable]
    public class PlayerPassiveStatsAbsolute : PlayerPassiveStats
    {
        public PlayerPassiveStatsAbsolute(float attackDamage, float blockingMovementSpeed, float movementSpeed, float specialAttackDamage) : base(attackDamage, blockingMovementSpeed, movementSpeed, specialAttackDamage)
        {

        }
    }

    [System.Serializable]
    public class PlayerStance
    {
        public readonly PlayerStanceType type;
        public readonly PlayerPassiveStatsRelative stats;

        public PlayerStance(PlayerStanceType type, PlayerPassiveStatsRelative stats)
        {
            this.type = type;
            this.stats = stats;
        }

        public static PlayerPassiveStatsAbsolute operator *(PlayerStance one, PlayerPassiveStatsAbsolute other)
        {
            return one.stats * other;
        }

        public static bool operator == (PlayerStance stance, PlayerStanceType type)
        {
            return stance.type == type;
        }

        public static bool operator !=(PlayerStance stance, PlayerStanceType type)
        {
            return !(stance.type == type);
        }

        public static bool operator ==(PlayerStanceType type, PlayerStance stance)
        {
            return stance.type == type;
        }

        public static bool operator !=(PlayerStanceType type, PlayerStance stance)
        {
            return !(stance.type == type);
        }
    }
}
