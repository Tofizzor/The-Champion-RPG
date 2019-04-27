using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class AttackButton : MonoBehaviour
    {
        public Stats.Skills skillAttackToPerfrom;


        public void PerfromAttack()
        {
            BaseAttack attk = (BaseAttack)skillAttackToPerfrom;
            GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(attk);
        }

       
    }
}
