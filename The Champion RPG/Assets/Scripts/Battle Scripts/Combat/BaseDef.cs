using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [CreateAssetMenu(menuName = "Player/Create Skill/Defence")]
    [System.Serializable]
    public class BaseDef : Stats.Skills
    {
        public int defenceStrength;

    }
}
