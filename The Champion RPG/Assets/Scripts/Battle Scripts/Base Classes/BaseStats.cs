using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BaseStats
    {
        public string userName;
        public int Level;
        public float MaxHP;
        public float CurHP;
        public float maxStamina;
        public float curStamina;
        public int strenght;
        public int fighting;
        public int agility;

        public List<Stats.Skills> userSkills = new List<Stats.Skills>();
        
    }
}
