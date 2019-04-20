using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [System.Serializable]
    public class BaseAttack
    {
        //name of the attack
        public string attackName;

        //base damage
        public int attackDamage;
        public int attackCost;
    }
}
