using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(menuName = "Player/Create Skill/Defence")]
    [System.Serializable]
    public class BaseDef : Stats.Skills
    {
        [Header("Battle Stats")]
        //name of the attack
        public string defenceName;

        //base damage
        public int defenceStrength;
        public int defenceCost;
    }
}
