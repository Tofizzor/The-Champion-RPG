using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu (menuName = "Player/Create Skill/Attack")]
    [System.Serializable]
    public class BaseAttack : Stats.Skills
    {
        //base damage
        public float attackDamage;


    }
}
