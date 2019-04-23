using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class DefendButton : MonoBehaviour
    {
        public Stats.Skills skillDefenceToPerfrom;

        public void PerfromDefence()
        {
            BaseDef deff = (BaseDef)skillDefenceToPerfrom;
            GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input3(deff);
        }
    }
}
