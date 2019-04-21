using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu (menuName = "Player/Create Skill/Attack")]
    [System.Serializable]
    public class BaseAttack : Stats.Skills
    {
        [Header("Battle Stats")]
        //name of the attack
        public string attackName;

        //base damage
        public int attackDamage;
        public int attackCost;
    }
}
