using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private BattleStateMachine BSM;
        public BaseEnemy enemy;
        public enum TurnState
        {
            PROCESSING,
            CHOOSEACTION,
            WAITING,
            ACTION,
            DEAD
        }

        public TurnState currentState;
        //variables for progress bar
        private float curCooldown;
        private float maxCooldown = 2f;

        //variable for enemy start location
        private Vector2 startPosition;

        //Time for action variables
        private bool actionStarted = false;
        public GameObject attackPlayer;
        private float animSpeed = 15f;
        //attack strenght
        private float skillDamage = 0;
        //defence strenght
        private float def_strength = 0;
        //alive
        private bool alive = true;

        // Start is called before the first frame update
        void Start()
        {
            currentState = TurnState.PROCESSING;
            BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
            startPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case (TurnState.PROCESSING):
                    UpgradeProgressBar();
                    break;

                case (TurnState.CHOOSEACTION):
                    ChooseAction();
                    currentState = TurnState.WAITING;
                    break;

                case (TurnState.WAITING):
                    break;

                case (TurnState.ACTION):
                    StartCoroutine(TimeForAction());
                    break;

                case (TurnState.DEAD):
                    if (!alive)
                    {
                        return;
                    }
                    else
                    {
                        StartCoroutine(enemyDefeated());
                        //enemy is not alive
                        alive = false;
                        //check alive
                        BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;

                    }
                    break;
            }
            userDefending(def_strength);
        }

        private IEnumerator enemyDefeated()
        {
            //enemy attacks are removed
            if (BSM.EnemysInBattle.Count > 0)
            {
                for (int i = 0; i < BSM.PerformList.Count; i++)
                {
                    if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                    {
                        BSM.PerformList.Remove(BSM.PerformList[i]);
                    }
                    if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                    {
                        BSM.PerformList[i].AttackersTarget = BSM.EnemysInBattle[Random.Range(0, BSM.EnemysInBattle.Count)];
                    }
                }
            }
            //remove from battle
            BSM.EnemysInBattle.Remove(this.gameObject);
            //after enemy dies change colour
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(225, 0, 0, 255);
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Transfer back to the main scene");

        }

        void UpgradeProgressBar()
        {
            curCooldown = curCooldown + Time.deltaTime;
            if (curCooldown >= maxCooldown)
            {
                currentState = TurnState.CHOOSEACTION;
            }
        }

        void ChooseAction()
        {
            HandleTurn myAttack = new HandleTurn();
            myAttack.Attacker = enemy.userName;
            myAttack.Type = "Enemy";
            myAttack.AttackersGameObject = this.gameObject;
            myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
            int num = Random.Range(0, enemy.userSkills.Count);
            myAttack.choosenAttack = enemy.userSkills[num];
            if(myAttack.choosenAttack.skillType == "Att")
            {
                BaseAttack attk = (BaseAttack)myAttack.choosenAttack;
                skillDamage = attk.attackDamage;
                Debug.Log(this.gameObject.name + " has chosen " + myAttack.choosenAttack.skillName + " with damage " + attk.attackDamage);
            }
            else if(myAttack.choosenAttack.skillType == "Def")
            {
                BaseDef deff = (BaseDef)myAttack.choosenAttack;
                def_strength = deff.defenceStrength;
            }
            BSM.CollectActions(myAttack);
        }


        private IEnumerator TimeForAction()
        {
            if (actionStarted)
            {
                yield break;
            }
            actionStarted = true;

            //enemy movement to the player
            if (BSM.PerformList[0].choosenAttack.skillType == "Att")
            {
                Vector2 playerPosition = new Vector2(attackPlayer.transform.position.x + 1f, attackPlayer.transform.position.y);
            while (MoveToEnemy(playerPosition))
            {
                yield return null;
            }
            //attack process
            yield return new WaitForSeconds(0.2f);
            //dealing damage
                DoDamage();


            }
            else if (BSM.PerformList[0].choosenAttack.skillType == "Def")
            {
                DoDefence();
            }
            //enemy movement back to the starting position
            Vector3 firstPosition = startPosition;
            while (MoveBack(firstPosition))
            {
                yield return null;
            }
            //action is removed from BSM
            BSM.PerformList.RemoveAt(0);
            //reset BSM
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
            //end the action stage
            actionStarted = false;

            //reset the enemy state
            curCooldown = 0f;
            currentState = TurnState.PROCESSING;

        }

        private bool MoveToEnemy(Vector3 distance)
        {
            return distance != (transform.position = Vector3.MoveTowards(transform.position, distance, animSpeed * Time.deltaTime));
        }

        private bool MoveBack(Vector3 distance)
        {
            return distance != (transform.position = Vector3.MoveTowards(transform.position, distance, animSpeed * Time.deltaTime));
        }

        void DoDamage()
        { 
            float dmg = enemy.strenght + skillDamage;
            attackPlayer.GetComponent<HeroStateMachine>().TakeDamage(dmg);
            def_strength = 0;
        }

        public void ReceiveDamage(float damageAmount)
        {
            float dmgAmount = damageAmount - def_strength;
            if (dmgAmount <= 0)
            {
                dmgAmount = 0;
            }
            enemy.CurHP -= dmgAmount;
            if(enemy.CurHP <= 0)
            {
                enemy.CurHP = 0;
                currentState = TurnState.DEAD;
            }
            def_strength = 0;
        }

        public void DoDefence()
        {
            def_strength += enemy.fighting;
        }

        //change the object to defence mode after using defence skill, otherwise stay normal
        public void userDefending(float checkdef)
        {
            if (checkdef == 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(100, 100, 255, 255);
            }
        }

    }
}
