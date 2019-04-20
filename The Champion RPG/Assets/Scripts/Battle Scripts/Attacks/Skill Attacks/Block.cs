using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Block : BaseAttack
    {
        public Block()
        {
            attackName = "Block";
            attackDamage = 0;
            attackCost = 5;
        }
    }
}
