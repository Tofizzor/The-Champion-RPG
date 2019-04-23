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
        private float skillDamage;
        private float animSpeed = 15f;

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
                    break;
            }
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
            Vector2 playerPosition = new Vector2(attackPlayer.transform.position.x + 1f, attackPlayer.transform.position.y);
            while (MoveToEnemy(playerPosition))
            {
                yield return null;
            }
            //attack process
            yield return new WaitForSeconds(0.2f);
            //dealing damage
            DoDamage();
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
            Debug.Log(dmg);
            attackPlayer.GetComponent<HeroStateMachine>().TakeDamage(dmg);
        }
    }
}
