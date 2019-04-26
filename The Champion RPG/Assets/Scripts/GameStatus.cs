using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStats
{
    public class GameStatus : MonoBehaviour
    {
        public static GameStatus gameSave;

        public List<GameObject> possibleEnemys = new List<GameObject>();
        public NPC findEnemy;
        public List<ScriptableObject> objects = new List<ScriptableObject>();
        public Vector2 playerPos;
        

        private void Awake()
        {
            if (gameSave == null)
            {
                gameSave = this;

            }
            else
            {
                Destroy(this.gameObject);
                
            }
            DontDestroyOnLoad(this);
            
        }

        
    }
}
