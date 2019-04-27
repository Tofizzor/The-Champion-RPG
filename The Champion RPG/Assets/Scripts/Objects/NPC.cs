using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC : Interactable
{
    [Header("Signal & Dialog")]
    public GameObject greetBox;
    public Text greetText;
    public string greetDialog;
    public GameObject dialogBox;
    public Text dialogText;
    public bool playerMet;
    public string metDialog;
    public BoolValue alreadyMet;
    public bool readyForAction = false;
    public BoolValue defeated;

    [Header("Fighting & XP gain")]
    public string EnemyTitle;
    public int XpGain;
    public Stats.PlayerStats pStats;

    public bool sceneTransition;
    public GameObject sceneTrans;

    bool battleAccess = false;

    // Start is called before the first frame update
    void Start()
    {
        if (sceneTransition)
        {
            sceneTrans.SetActive(false);
        }
        playerMet = alreadyMet.runTimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && battleAccess)
        //{
        //    if (playerInRange && readyForAction == true && !greetBox.activeSelf && sceneTransition)
        //    {
        //        sceneTrans.SetActive(true);
        //        readyForAction = false;
        //        GetXP();
        //    }
        //    battleAccess = false;
        //}
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            battleAccess = true;
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                if (battleAccess)
                {
                    if (playerInRange && readyForAction == true && !greetBox.activeSelf && sceneTransition)
                    {
                        if (!defeated.runTimeValue)
                        {
                            sceneTrans.SetActive(true);
                            GetXP();
                            readyForAction = false;
                            battleAccess = false;
                        }
                        
                    }
                }
            }
            else if (!playerMet)
            {
                GreetPlayer();
                pStats.gameObject.GetComponent<PlayerMovement>().currentState = PlayerState.interact;
            }
            else
            {
                RepeatDialog();
            }
        }


    }

    public void GreetPlayer()
    {
        greetText.text = greetDialog;
        readyForAction = true;
        //Dialog Window on
        greetBox.SetActive(true);
        //Player has already interacted with the NPC
        playerMet = true;
        alreadyMet.runTimeValue = playerMet;
    }

    public void RepeatDialog()
    {
        dialogText.text = metDialog;
        //Show dialog box
        dialogBox.SetActive(true);

    }

    //Methods to detect if player is in range of the sign
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameStats.GameStatus.gameSave.findEnemy = this.gameObject.GetComponent<NPC>();
            GameStats.GameStatus.gameSave.playerPos = collision.transform.position;
            dialogBox.SetActive(false);
            playerInRange = true;
            Enable();
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameStats.GameStatus.gameSave.findEnemy = null;
            dialogBox.SetActive(false);
            playerInRange = false;
            Disable();
        }
    }
    
    private void GetXP()
    {
        pStats.UpdateXP(XpGain);
    }

}
