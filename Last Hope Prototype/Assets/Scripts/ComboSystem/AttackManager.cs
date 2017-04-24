using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.ComboSystem
{
    //Singleton so we have same list of attacks in all game. We can add each attack only once 
    //(using attack's name as key for it). 
    public class AttackManager
    {
        Dictionary<String, Attack> attacks;
        private static AttackManager instance = new AttackManager();

        private AttackManager()
        {
            //TODO: Add differents attacks here...or use AddAttack method better ¿?
            //We have to know about 
        }

        public static AttackManager Instance
        {
            get
            {
                return instance;
            }
        }

        //To Add Differents Attacks (util for getting other colliders)!
        public bool AddAttack(String name, int damage, int bufferOffset, int colliderStart, int colliderEnd, Collider collider = null, AnimationClip animation = null)
        {
            if (attacks.ContainsKey(name))
                return false;

            Attack attack = new Attack(name, damage, bufferOffset, colliderStart, colliderEnd, collider, animation);
            attacks.Add(attack.Name, attack);

            return true;
        }

        //To add/change collider in an attack
        public void AddColliderToAttack(String name, Collider collider)
        {
            if (attacks.ContainsKey(name))
            {
                attacks[name].Collider = collider;
            }
        }

        public Attack GetAttack(String name)
        {
            if (attacks.ContainsKey(name))
            {
                return attacks[name];
            }

            return null;
        }
    }
}
