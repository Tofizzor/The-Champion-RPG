using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    interact
}

public class PlayerMovement : MonoBehaviour {

    // Speed of the movement 
    public float speed;

    // Variable to set the players current state
    public PlayerState currentState;

    // Image that is shown when item is received
    public SpriteRenderer receivedItemSprite;
    public Inventory playerInventory;

    private Rigidbody2D myRigidbody;
    private Vector3 change;

    // Movement animation
    private Animator animator;

    //Spawning location in scene
    public VectorValue startingPosition;



	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        //Spawn player facing down when game starts
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        //Spawning location in scene
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update () {
        // Check if player is in interaction
        if(currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        AnimatedMovement();
        
	}

    void AnimatedMovement()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);

        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        //Diagnal walking speed fix
        change.Normalize();

        myRigidbody.MovePosition(
            transform.position + change * speed  * Time.deltaTime);
    }

    public void GetItem()
    {
        //If there is current item then do receive item
        if (playerInventory.currentItem != null)
        {

            if (currentState != PlayerState.interact)
            {
                // Animation for interact state
                //animator.SetBool("Receive Item", true);
                // Set the current state
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

}
