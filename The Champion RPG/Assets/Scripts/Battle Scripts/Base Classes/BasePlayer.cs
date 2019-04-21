using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    [System.Serializable]
    public class BasePlayer: BaseStats
    {
        public int playerXP;

        public void GetStats(Stats.PlayerStats attr)
        {
            userName = attr.PlayerName;
            MaxHP = attr.PlayerMaxHP;
            CurHP = attr.PlayerCurHP;
            Level = attr.PlayerLevel;
            playerXP = attr.PlayerXP;
            userSkills = attr.PlayerSkills;
            List<Stats.PlayerAttributes>.Enumerator attrib = attr.Attributes.GetEnumerator();
            while (attrib.MoveNext())
            {
                if (attrib.Current.attribute.name.ToString() == "Strength")
                {
                    strenght = attrib.Current.amount;

                }
                if (attrib.Current.attribute.name.ToString() == "Fighting")
                {
                    fighting = attrib.Current.amount;

                }
                if (attrib.Current.attribute.name.ToString() == "Agility")
                {
                    agility = attrib.Current.amount;

                }
            }
        }



    }
}