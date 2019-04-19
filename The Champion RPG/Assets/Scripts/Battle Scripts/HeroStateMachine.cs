using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle{
public class HeroStateMachine : MonoBehaviour
{
        private BattleStateMachine BSM;
        public Stats.PlayerStats pStats;
        public BaseHero player;
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

    // Start is called before the first frame update
    void Start()
    {
            player.GetStats(pStats);
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
}
}
