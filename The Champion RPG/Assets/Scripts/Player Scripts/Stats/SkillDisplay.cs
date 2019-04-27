using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stats
{
    public class SkillDisplay : MonoBehaviour
    {
        [Header("Skill SO")]
        public Skills skill;
        //Get UI components
        public Text skillName;
        public Text skillDescription;
        public Image skillIcon;
        public Text skillLevel;
        public Text skillXPNeeded;
        public Text skillAttribute;
        public Text skillAttrAmount;


        [SerializeField]
        private PlayerStats m_PlayerHandler;



        // Start is called before the first frame update
        void Start()
        {
            m_PlayerHandler = this.GetComponentInParent<PlayerHandler>().Player;
            //XP listener
            m_PlayerHandler.onXPChange += ReactToChange;
            //Level listener
            m_PlayerHandler.onLevelChange += ReactToChange;


            skill.SetValues(this.gameObject, m_PlayerHandler);


        }

        void Update()
        {
            EnableSkills();
        }

        public void EnableSkills()
        {
            //show the skills which already learned
            if(m_PlayerHandler && skill && !skill.EnableSkill(m_PlayerHandler))
            {
                EnabledSkillIcon();

            }
            
            else if (m_PlayerHandler && skill && skill.EnableSkill(m_PlayerHandler) && skill.CheckSkills(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
            else
            {
                TurnOffSkillIcon();
            }
        }

        private void OnEnable()
        {

                EnableSkills();

        }

        public void GetSkill()
        {
            if (skill.GetSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
        }

        private void TurnOnSkillIcon()
        {
            this.GetComponent<Button>().interactable = true;
            var colors = GetComponent<Button>().colors;
            colors.normalColor = new Color32(3, 220, 0, 255);
            this.GetComponent<Button>().colors = colors;

        }

        private void TurnOffSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            var colors = GetComponent<Button>().colors;
            colors.disabledColor = new Color32(165, 1, 1, 255);
            this.GetComponent<Button>().colors = colors;

        }

        private void EnabledSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            var colors = GetComponent<Button>().colors;
            colors.disabledColor = new Color32(80, 120, 255, 200);
            this.GetComponent<Button>().colors = colors;


        }

        void ReactToChange()
        {
            EnableSkills();
        }

    }
}
