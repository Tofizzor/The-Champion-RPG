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
        private float maxCooldown = 1f;

        //variable for enemy start location
        private Vector3 startPosition;

        //Time for action variables
        private bool actionStarted = false;
        public GameObject attackPlayer;
        private float animSpeed = 3f;

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
            myAttack.Attacker = enemy.enemyName;
            myAttack.Type = "Enemy";
            myAttack.AttackersGameObject = this.gameObject;
            myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
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
            Vector3 playerPosition = new Vector3(attackPlayer.transform.position.x + 1.5f, attackPlayer.transform.position.y, attackPlayer.transform.position.z);
            while (MoveToEnemy(playerPosition))
            {
                yield return null;
            }
            //attack process
            yield return new WaitForSeconds(0.5f);
            //dealing damage

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
    }
}
