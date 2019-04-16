using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable {

    [Header ("Contents")]
    public Item itemContains;
    public Inventory playerInventory;

    [Header("Signal & State & Dialog")]
    public bool isOpen;
    public BoolValue storedOpen;
    public Signal GetItem;
    public GameObject dialogBox;
    public Text dialogText;


	// Use this for initialization
	void Start () {
        isOpen = storedOpen.runTimeValue;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
           if(!isOpen)
            {
                OpenChest();

            }
            else
            {
                ChestOpened();
            }
        }

    }
    public void OpenChest()
    {
        // Dialog Window on
        dialogBox.SetActive(true);
        // dialog text = contents text
        dialogText.text = itemContains.itemDescription;
        // add contents to the inventory
        playerInventory.AddItem(itemContains);
        playerInventory.currentItem = itemContains;
        // raise the signal to the player
        GetItem.Raise();
        // set the chest to opened
        isOpen = true;
        // interaction clue no longer appears
        Disable();
        // remembers that the chest was opened
        storedOpen.runTimeValue = isOpen;
    }

    public void ChestOpened()
    {

            // Dialog off
            dialogBox.SetActive(false);
            // raise the signal to the player for animation off
            GetItem.Raise();

    }

    //Methods to detect if player is in range of the sign
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpen)
        {

            playerInRange = true;
            Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            playerInRange = false;
            Disable();
        }
    }


}
