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
        public Vector2 playerPosition;
        public VectorValue playerStorage;
        private bool alive;
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
            
            /*
                if (collision.CompareTag("Player") && SceneManager.GetActiveScene().name == "BattleScene")
            {
                if (collision.GetComponent<Battle.HeroStateMachine>().playerAlive)
                {
                    playerStorage.initialValue = GameStats.GameStatus.gameSave.playerPos;
                    Debug.Log("Stayed alive");

                }
            }
            if (SceneManager.GetActiveScene().name == "BattleScene" && collision.CompareTag("Enemy"))
            {
                if (collision.GetComponent<Battle.EnemyStateMachine>().currentState == Battle.EnemyStateMachine.TurnState.DEAD)
                {
                    Debug.Log("Enemy is Dead");
                    sceneToLoad = collision.GetComponent<Battle.EnemyStateMachine>().lastScene;
                    //playerStorage.initialValue = GameStats.GameStatus.gameSave.playerPos;
                }
            }
            */

            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                if (SceneManager.GetActiveScene().name == "BattleScene")
                {
                    alive = collision.GetComponent<Battle.HeroStateMachine>().playerAlive;
                }
                playerStorage.initialValue = playerPosition;
                StartCoroutine(FadeCo());
                SceneManager.LoadScene(sceneToLoad);
            }
            if (SceneManager.GetActiveScene().name == "BattleScene" && alive)
            {
                    if (collision.CompareTag("Player"))
                    {
                        playerStorage.initialValue = GameStats.GameStatus.gameSave.playerPos;
                    }
                    if (collision.CompareTag("Enemy"))
                    {
                        sceneToLoad = collision.GetComponent<Battle.EnemyStateMachine>().lastScene;
                        StartCoroutine(FadeCo());
                        SceneManager.LoadScene(sceneToLoad);
                    }
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
