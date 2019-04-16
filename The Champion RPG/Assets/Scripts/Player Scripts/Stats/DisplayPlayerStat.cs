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
                ShowXP.text = m_PlayerHandler.PlayerXP.ToString() + " XP";

            if (ShowLVL)
                ShowLVL.text = m_PlayerHandler.PlayerLevel.ToString() + " LVL";
        }
    }
}
