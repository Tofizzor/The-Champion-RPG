using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStats
{
    public class SceneTransition : MonoBehaviour
    {

        [Header("New scene variables")]
        public string sceneToLoad;
        public VectorValue playerStorage;
        //public VectorValue cameraMin;
        //public VectorValue cameraMax;

        [Header("Transition Variables")]
        public GameObject fadeInPanel;
        public GameObject fadeOutPanel;
        public float fadeWait;


        private void Awake()
        {
            if (fadeInPanel != null)
            {
                GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
                Destroy(panel, 1f);
            }

        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                StartCoroutine(FadeCo());
                //SceneManager.LoadScene(sceneToLoad);

            }
            if (collision.CompareTag("Enemy"))
            {

            }
        }

        //When entering new scene show fade panel
        public IEnumerator FadeCo()
        {
            if (fadeOutPanel != null)
            {
                Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
            }
            yield return new WaitForSeconds(fadeWait);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }


    }
}
