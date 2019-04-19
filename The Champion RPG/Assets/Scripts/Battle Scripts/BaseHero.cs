using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseHero
    {
        public string playerName;
        public int playerLevel;
        public int playerXP;
        public int strenght;
        public int fighting;
        public int agility;

        public void GetStats(Stats.PlayerStats attr)
    {
        playerName = attr.PlayerName;
        playerLevel = attr.PlayerLevel;
        playerXP = attr.PlayerXP;
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

