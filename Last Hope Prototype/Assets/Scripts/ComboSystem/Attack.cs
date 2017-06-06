//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using UnityEngine;

//namespace Assets.Scripts.ComboSystem
//{
//    public class Attack
//    {
//        String name = "";
//        int damage = 0;
//        //Window to get keys pressed by the player
//        int bufferOffset = 0;
//        //Between this values of time, colliders must be active. Otherwise they must be inactive
//        int colliderStart = 0;
//        int colliderEnd = 0;

//        Collider collider;
//        //¿Clip, Animation, ...?
//        AnimationClip animation;

//        Dictionary<String, Attack> candidateAttacks;
//        //So we can know in other classes which keys we have to filter for this attack
//        List<String> filteredKeys;

//        public Attack()
//        {

//        }

//        public Attack(String name, int damage, int bufferOffset, int colliderStart, int colliderEnd, Collider collider = null, AnimationClip animation = null)
//        {
//            this.Name = name;
//            this.damage = damage;
//            this.bufferOffset = bufferOffset;
//            this.colliderStart = colliderStart;
//            this.colliderEnd = colliderEnd;

//            this.Collider = collider;
//            this.Animation = animation;
//            candidateAttacks = new Dictionary<string, Attack>();
//            filteredKeys = new List<string>();
//        }

        

//        public void AddCandidateAttack(String key, Attack attack)
//        {
//            if(candidateAttacks.ContainsKey(key) == false)
//            {
//                filteredKeys.Add(key);
//                candidateAttacks.Add(key, attack);
//            }
//        }

//        //Return next attack if we can do an attack from this with key pressed. Otherwise it returns null
//        public Attack NextAttack(String key)
//        {
//            if (candidateAttacks.ContainsKey(key))
//            {
//                return candidateAttacks[key];
//            }

//            return null;
//        }

//        public List<String> FilteredKeys()
//        {
//            return filteredKeys;
//        }

//        public Collider Collider
//        {
//            get
//            {
//                return collider;
//            }

//            set
//            {
//                collider = value;
//            }
//        }

//        public AnimationClip Animation
//        {
//            get
//            {
//                return animation;
//            }

//            set
//            {
//                animation = value;
//            }
//        }

//        public string Name
//        {
//            get
//            {
//                return name;
//            }

//            set
//            {
//                name = value;
//            }
//        }


//    }
//}
