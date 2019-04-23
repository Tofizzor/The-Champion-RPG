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
        public GameObject SelectPanel;
        public GameObject AttackPanel;
        public GameObject DefendPanel;
        public Transform selectSpacer;
        public Transform attackSpacer;
        public Transform defendSpacer;
        public GameObject selectButton;
        public GameObject attButton;
        public GameObject defButton;
        private List<GameObject> attButtons = new List<GameObject>();
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
            SelectPanel.SetActive(false);
            AttackPanel.SetActive(false);
            DefendPanel.SetActive(false);
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
                        SelectPanel.SetActive(true);
                        //create button
                        CreateAttackButtons();
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
            SelectPanel.SetActive(false);
            HeroInput = HeroGUI.DONE;
        }
        

        public void Input2(BaseAttack attSkill)//choosen defence action
        {
            HeroChoice.Attacker = HerosToManage[0].name;
            HeroChoice.AttackersGameObject = HerosToManage[0];
            HeroChoice.Type = "Player";
            HeroChoice.AttackersTarget = EnemysInBattle[0];
            HeroChoice.choosenAttack = attSkill;
            AttackPanel.SetActive(false);
            HeroInput = HeroGUI.DONE;

        }

        public void Input3(BaseDef defSkill)//choosen defence action
        {
            HeroChoice.Attacker = HerosToManage[0].name;
            HeroChoice.AttackersGameObject = HerosToManage[0];
            HeroChoice.Type = "Player";
            HeroChoice.AttackersTarget = EnemysInBattle[0];
            HeroChoice.choosenAttack = defSkill;
            AttackPanel.SetActive(false);
            HeroInput = HeroGUI.DONE;

        }

        public void toAttackPanel()
        {
            SelectPanel.SetActive(false);
            AttackPanel.SetActive(true);
        }
        public void toDefendPanel()
        {
            SelectPanel.SetActive(false);
            DefendPanel.SetActive(true);
        }

        void PlayerInputDone()
        {
            PerformList.Add(HeroChoice);
            HerosToManage.RemoveAt(0);
            HeroInput = HeroGUI.ACTIVATE;
            foreach(GameObject attButton in attButtons)
            {
                Destroy(attButton);
            }
            attButtons.Clear();
        }

        //create action buttons
        void CreateAttackButtons()
        {
            GameObject AttackButton = Instantiate(selectButton) as GameObject;
            Text AttackText = AttackButton.transform.Find("Text").gameObject.GetComponent<Text>();
            AttackText.text = "Attack";
            AttackButton.GetComponent<Button>().onClick.AddListener(() => toAttackPanel());
            AttackButton.transform.SetParent(selectSpacer, false);
            attButtons.Add(AttackButton);

            if(HerosToManage[0].GetComponent<HeroStateMachine>().player.userSkills.Count > 0)
            {       
                foreach(Stats.Skills skillatk in HerosToManage[0].GetComponent<HeroStateMachine>().player.userSkills)
                {
                    if (skillatk.skillType == "Att")
                    {
                        GameObject AttButton = Instantiate(attButton) as GameObject;
                        Text SkillButtonText = AttButton.transform.Find("Text").gameObject.GetComponent<Text>();
                        SkillButtonText.text = skillatk.skillName;
                        AttackButton ATB = AttButton.GetComponent<AttackButton>();
                        ATB.skillAttackToPerfrom = skillatk;
                        AttButton.transform.SetParent(attackSpacer, false);
                        attButtons.Add(AttButton);
                    }
                    
                }
            }
            else
            {
                AttackButton.GetComponent<Button>().interactable = false;
            }
            
            
            GameObject DefendButton = Instantiate(selectButton) as GameObject;
            Text DefendText = DefendButton.transform.Find("Text").gameObject.GetComponent<Text>();
            DefendText.text = "Defend";
            DefendButton.GetComponent<Button>().onClick.AddListener(() => toDefendPanel());
            DefendButton.transform.SetParent(selectSpacer, false);
            attButtons.Add(DefendButton);
            if (HerosToManage[0].GetComponent<HeroStateMachine>().player.userSkills.Count > 0)
            {
                foreach (Stats.Skills skilldef in HerosToManage[0].GetComponent<HeroStateMachine>().player.userSkills)
                {
                    if (skilldef.skillType == "Def")
                    {
                        GameObject DefButton = Instantiate(defButton) as GameObject;
                        Text DefButtonText = DefButton.transform.Find("Text").gameObject.GetComponent<Text>();
                        DefButtonText.text = skilldef.skillName;
                        DefButton.transform.SetParent(defendSpacer, false);
                        attButtons.Add(DefButton);
                        //AttackButton ATB = SkillButton.GetComponent<AttackButton>();
                        //ATB.skillAttackToPerfrom = skillatk;
                    }

                }
            }
            else
            {
                DefendButton.GetComponent<Button>().interactable = false;
            }
            


        }

    }
}
