using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour {


    public Vector2 cameraChange;
    public Vector3 playerChange;
    //Variables for text display after changing room
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    //Calling camera movement script
    private CameraMovement cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main.GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
      

    }

    //When player is in trigger zone, access camera and change the offset
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;

            if (needText)
            {
                StartCoroutine(placeNameCo());
            }

        }
    }
        //Method that have specified wait time
        private IEnumerator placeNameCo()
        {
            text.SetActive(true);
            placeText.text = placeName;
            needText = false;
            yield return new WaitForSeconds(2f);
            text.SetActive(false);
            needText = false;
        }
    
}
