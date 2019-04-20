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

    [Header("Fighting & XP gain")]
    public int XpGain;
    public Stats.PlayerStats pStats;


    // Start is called before the first frame update
    void Start()
    {
        playerMet = alreadyMet.runTimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else if (!playerMet)
            {
                GreetPlayer();
                greetText.text = greetDialog;

            }
            else
            {

                RepeatDialog();
                dialogText.text = metDialog;
            }
        }

    }

    public void GreetPlayer()
    {
        //Dialog Window on
        greetBox.SetActive(true);
        //Player has already interacted with the NPC
        playerMet = true;
        alreadyMet.runTimeValue = playerMet;
        GetXP();
    }

    public void RepeatDialog()
    {   
        //Show dialog box
        dialogBox.SetActive(true);

    }

    //Methods to detect if player is in range of the sign
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogBox.SetActive(false);
            playerInRange = true;
            Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
