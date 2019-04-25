using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stats
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Player Stats")]
        public string PlayerName;
        public int PlayerMaxHP = 50;
        public int PlayerCurHP;

        //assign listener for player XP
        [SerializeField]
        private int m_PlayerXP = 0;
        public int PlayerXP
        {
            get { return m_PlayerXP; }
            set
            {
                m_PlayerXP = value;

                //check if xp is changed
                if (onXPChange != null)
                    onXPChange();

            }
        }



        //assign listener for player level
        [SerializeField]
        private int m_PlayerLevel = 1;
        public int PlayerLevel
        {
            get { return m_PlayerLevel; }
            set
            {
                m_PlayerLevel = value;

                if (onLevelChange != null)
                    onLevelChange();
            }
        }
        public Inventory playerInventory;
        int numOfItems = 0;

        [Header("Player Attributes")]
        public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();

        [Header("Player Skills")]
        public List<Skills> PlayerSkills = new List<Skills>();

        //Delegates for listeners
        public delegate void OnXPChange();
        public event OnXPChange onXPChange;

        public delegate void OnLevelChange();
        public event OnLevelChange onLevelChange;

        public void UpdateLevel(int amount)
        {
            PlayerLevel += amount;
        }

        public void UpdateXP(int amount)
        {
            PlayerXP += amount;
        }


        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "MainScene")
            {
                if (playerInventory.items.Count > 0 && numOfItems < playerInventory.items.Count)
                {
                    for (int a = 0; a < Attributes.Count; a++)
                    {
                        for (int i = numOfItems; i < playerInventory.items.Count; i++)
                        {
                            if (Attributes[a].attribute.name == "Strength")
                            {
                                Attributes[a].amount += playerInventory.items[i].Strength;
                                Debug.Log("Strength" + Attributes[a].amount);
                            }
                            if (Attributes[a].attribute.name == "Agility")
                            {
                                Attributes[a].amount += playerInventory.items[i].Agility;
                                Debug.Log("Agility" + Attributes[a].amount);
                            }

                        }
                    }
                    numOfItems += 1;
                }
            }

        }
    }
}
