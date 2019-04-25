using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stats
{

    public class EnemyInfo : MonoBehaviour
    {
        public Text ShowUserTitle;

        [SerializeField]
        private Battle.EnemyStateMachine enemStats;

        // Update is called once per frame
        void Update()
        {
            ShowEnemyInfo();
        }

        void ShowEnemyInfo()
        {
            if (ShowUserTitle)
            {
                ShowUserTitle.text = enemStats.enemy.enemyTitle;
            }
        }
    }
}
