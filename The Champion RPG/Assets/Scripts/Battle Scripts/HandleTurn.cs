using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    //attacker name
    public string Attacker;
    [Header("Attacker")]
    public GameObject AttackersGameObject;
    [Header("Defender")]
    public GameObject AttackersTarget;

}
