using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stats
{

    public class DisplayPlayerStat : MonoBehaviour
    {
        public Text ShowPlayerName;
        public Text ShowXP;
        public Text ShowLVL;
        public Text ShowSTR;
        public Text ShowAGL;
        public Text ShowFGH;

        [SerializeField]
        private PlayerStats m_PlayerHandler;
        

        void Update()
        {
            ShowPlayerInfo();

        }

        void ShowPlayerInfo()
        {
            if (ShowPlayerName)
                ShowPlayerName.text = m_PlayerHandler.PlayerName.ToString();

            if (ShowXP)
                ShowXP.text = m_PlayerHandler.PlayerXP.ToString();

            if (ShowLVL)
                ShowLVL.text = m_PlayerHandler.PlayerLevel.ToString();

            for(int i = 0; i < m_PlayerHandler.Attributes.Count; i++)
            {
                if(m_PlayerHandler.Attributes[i].attribute.name == "Strength")
                {

                    if (ShowSTR)
                        ShowSTR.text = m_PlayerHandler.Attributes[i].amount.ToString();
                }
                if(m_PlayerHandler.Attributes[i].attribute.name == "Agility")
                {
                    if (ShowAGL)
                        ShowAGL.text = m_PlayerHandler.Attributes[i].amount.ToString();
                }
                if (m_PlayerHandler.Attributes[i].attribute.name == "Fighting")
                {
                    if (ShowFGH)
                        ShowFGH.text = m_PlayerHandler.Attributes[i].amount.ToString();
                }
            }
            
        }
    }
}
