using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [System.Serializable]
    public class HandleTurn
    {
        //attacker name
        public string Attacker;
        //enemy or player
        public string Type;
        [Header("Attacker")]
        public GameObject AttackersGameObject;
        [Header("Defender")]
        public GameObject AttackersTarget;

    }
}
