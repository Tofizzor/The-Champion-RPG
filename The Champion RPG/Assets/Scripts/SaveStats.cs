using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStats : MonoBehaviour
{
    
    public Stats.PlayerStats pStats;
    //Static lists that stores skills and attributes
    static public List<Stats.Skills> SaveSkills = new List<Stats.Skills>();
    static public List<Stats.PlayerAttributes> SaveAttributes = new List<Stats.PlayerAttributes>();

    //bool variables for checking state
    static private bool restart = true;
    static private bool attSaved = false;

    private void Awake()
    {
        //check if game has been restarted to keep data from being saved forever
        if (restart == true)
        {
            PlayerPrefs.DeleteAll();
            SaveSkills.Clear();
            SaveAttributes.Clear();
        }
        onStart(pStats);
        //check if attributes were saved previously, prevents from putting empty list of attributes at the start
        if (attSaved == true)
        {
            pStats.Attributes = SaveAttributes;
        }
    }

    private void OnDestroy()
    {
        saveStatus(pStats);
    }

    public void onStart(Stats.PlayerStats pStats)
    {
        //Get player status from prefs
        pStats.PlayerName = PlayerPrefs.GetString("name", pStats.PlayerName);
        pStats.PlayerLevel = PlayerPrefs.GetInt("level", pStats.PlayerLevel);
        pStats.PlayerXP = PlayerPrefs.GetInt("xp", pStats.PlayerXP);
        pStats.PlayerMaxHP = PlayerPrefs.GetInt("hp", pStats.PlayerMaxHP);
        pStats.PlayerCurHP = PlayerPrefs.GetInt("crhp", pStats.PlayerCurHP);
        //Delete saved files
        PlayerPrefs.DeleteAll();
        pStats.PlayerSkills = SaveSkills;
    }

    public void saveStatus(Stats.PlayerStats pStats)
    {

        //Set player status from prefs
        PlayerPrefs.SetString("name", pStats.PlayerName);
        PlayerPrefs.SetInt("level", pStats.PlayerLevel);
        PlayerPrefs.SetInt("xp", pStats.PlayerXP);
        PlayerPrefs.SetInt("hp", pStats.PlayerMaxHP);
        PlayerPrefs.SetInt("crhp", pStats.PlayerCurHP);
        //Collect player learned skills and attributes
        SaveSkills = pStats.PlayerSkills;
        restart = false;
        SaveAttributes = pStats.Attributes;
        attSaved = true;

    }

}
