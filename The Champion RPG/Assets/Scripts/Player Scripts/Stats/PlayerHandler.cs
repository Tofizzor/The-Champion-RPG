using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    public class PlayerHandler : MonoBehaviour
    {
        public PlayerStats Player;

        [SerializeField]
        private Canvas m_Canvas;
        private bool m_SeeCanvas;

        // Update is called once per frame
        void Update()
        {
            //checks if button e is pressed by user
            if (Input.GetKeyDown("e"))
            {
                if (m_Canvas)
                {
                    //changes the state of SeeCanvas
                    m_SeeCanvas = !m_SeeCanvas;
                    //sets the SeeCanvas state to active 
                    m_Canvas.gameObject.SetActive(m_SeeCanvas);
                }
            }


        }
    }
}