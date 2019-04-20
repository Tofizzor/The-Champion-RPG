using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle{
public class HeroStateMachine : MonoBehaviour
{
        private BattleStateMachine BSM;
        public Stats.PlayerStats pStats;
        public BasePlayer player;
        public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

        public TurnState currentState;
        //variables for progress bar
        private float curCooldown;
        private float maxCooldown = 1f;
        public Image ProgressBar;
        //IeNumerator
        public GameObject EnemyToAttack;
        private bool actionStarted = false;
        private Vector2 startPosition;
        private float animSpeed = 10f;


    // Start is called before the first frame update
        void Start()
    {
            player.GetStats(pStats);
            startPosition = transform.position;
            //how fast does the progress bar charge
            curCooldown = Random.Range(0, 2.5f);
            BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
            currentState = TurnState.PROCESSING;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;

            case (TurnState.ADDTOLIST):
                    BSM.HerosToManage.Add(this.gameObject);
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
        float calcCooldown = curCooldown / maxCooldown;
        ProgressBar.transform.localScale = new Vector2(Mathf.Clamp(calcCooldown, 0, 1), ProgressBar.transform.localScale.y);
        if (curCooldown >= maxCooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

        private IEnumerator TimeForAction()
        {
            if (actionStarted)
            {
                yield break;
            }
            actionStarted = true;

            //enemy movement to the player
            Vector2 enemyPosition = new Vector2(EnemyToAttack.transform.position.x - 1.5f, EnemyToAttack.transform.position.y);
            while (MoveToEnemy(enemyPosition))
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
