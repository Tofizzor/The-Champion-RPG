using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStats
{
    public class GameStatus : MonoBehaviour
    {
        
        //Store attributes and skills in arrays that are static
        static public List<Stats.Skills> SaveSkills = new List<Stats.Skills>();
        static public List<Stats.PlayerAttributes> SaveAttributes = new List<Stats.PlayerAttributes>();

        public void onStart(Stats.PlayerStats pStats)
        {
            //Get player status from prefs
            pStats.PlayerName = PlayerPrefs.GetString("name", pStats.PlayerName);
            pStats.PlayerLevel = PlayerPrefs.GetInt("level", pStats.PlayerLevel);
            pStats.PlayerXP = PlayerPrefs.GetInt("xp", pStats.PlayerXP);
            //Delete saved files
            PlayerPrefs.DeleteAll();
            pStats.PlayerSkills = SaveSkills;
            SaveAttributes = pStats.Attributes;

        }

        public void saveStatus(Stats.PlayerStats pStats)
        {
            //Set player status from prefs
            PlayerPrefs.SetString("name", pStats.PlayerName);
            PlayerPrefs.SetInt("level", pStats.PlayerLevel);
            PlayerPrefs.SetInt("xp", pStats.PlayerXP);
            //Collect player learned skills and attributes
            SaveSkills = pStats.PlayerSkills;
            SaveAttributes = pStats.Attributes;

        }
    }
}
