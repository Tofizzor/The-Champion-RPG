using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle
{
    public class BattleStateMachine : MonoBehaviour
    {   
        //Stages for the battle actions
        public enum PerformAction
        {
            WAIT,
            TAKEACTION,
            PERFORMACTION
        }

        //Stages for Player GUI control
        public enum HeroGUI
        {
            ACTIVATE,
            WAITING,
            INPUT1,
            //INPUT2,
            DONE
        }
        public HeroGUI HeroInput;
        public GameObject AttackPanel;
        //public GameObject EnemySelectPanel;

        //List for future improvements where player has teamates
        public List<GameObject> HerosToManage = new List<GameObject>();
        private HandleTurn HeroChoice;
        //public GameObject enemyButton; buttons that will select different enemy
        //public Transform Spacer;

        public PerformAction battleStates;

        //List for handling turns
        public List<HandleTurn> PerformList = new List<HandleTurn>();
        //List for future improvements where there is more enemys and player teammates
        public List<GameObject> HerosInBattle = new List<GameObject>();
        public List<GameObject> EnemysInBattle = new List<GameObject>();



        // Start is called before the first frame update
        void Start()
        {
            battleStates = PerformAction.WAIT;
            EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
            HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            HeroInput = HeroGUI.ACTIVATE;
            //call function to create buttons for different enemys
            //EnemyButtons();
            AttackPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

            //Enemy stages
            switch (battleStates)
            {
                case (PerformAction.WAIT):
                    if (PerformList.Count > 0)
                    {
                        battleStates = PerformAction.TAKEACTION;
                    }
                    break;

                case (PerformAction.TAKEACTION):
                    GameObject performer = GameObject.Find(PerformList[0].Attacker);
                    if (PerformList[0].Type == "Enemy")
                    {
                        EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                        ESM.attackPlayer = PerformList[0].AttackersTarget;
                        ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                    }

                    if (PerformList[0].Type == "Player")
                    {
                        HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                        HSM.EnemyToAttack = PerformList[0].AttackersTarget;
                        HSM.currentState = HeroStateMachine.TurnState.ACTION;
                    }
                    battleStates = PerformAction.PERFORMACTION;
                    break;

                case (PerformAction.PERFORMACTION):
                    break;
            }

            //Player stages
            switch (HeroInput)
            {
                case (HeroGUI.ACTIVATE):
                    if(HerosToManage.Count > 0)
                    {
                        HeroChoice = new HandleTurn();
                        AttackPanel.SetActive(true);
                        HeroInput = HeroGUI.WAITING;
                    }
                    break;
                case (HeroGUI.WAITING):
                    //idle state
                    break;
                case (HeroGUI.DONE):
                    PlayerInputDone();
                    break;
            }

        }

        public void CollectActions(HandleTurn input)
        {
            PerformList.Add(input);
        }

        //Function for further improvements where there will be GUI for selecting attack from multiple enemys
        /*
        void EnemyButtons()
        {
            foreach(GameObject enemy in EnemysInBattle)
            {
                GameObject newButton = Instantiate(enemyButton) as GameObject;
                EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

                EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine>();

                Text buttonText = newButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
                buttonText.text = cur_enemy.enemy.name;

                button.EnemyPrefab = enemy;

                newButton.transform.SetParent (Spacer,false);
            }
        }
        */

        //Attack button
        public void Input1()
        {
            HeroChoice.Attacker = HerosToManage[0].name;
            HeroChoice.AttackersGameObject = HerosToManage[0];
            HeroChoice.Type = "Player";
            HeroChoice.AttackersTarget = EnemysInBattle[0];
            AttackPanel.SetActive(false);
            HeroInput = HeroGUI.DONE;
        }

        void PlayerInputDone()
        {
            PerformList.Add(HeroChoice);
            HerosToManage.RemoveAt(0);
            HeroInput = HeroGUI.ACTIVATE;
        }

    }
}
