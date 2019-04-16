using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWorld : MonoBehaviour
{
    public GameObject virtualCamera;

    //Variables for text display after changing room
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("Player"))
        {

                virtualCamera.SetActive(true);

                if (needText)
                {
                    StartCoroutine(placeNameCo());
                }
            }

        

    }

    public virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")){
            virtualCamera.SetActive(false);
            
        }
    }

    //Method that have specified wait time
    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);

    }

}
