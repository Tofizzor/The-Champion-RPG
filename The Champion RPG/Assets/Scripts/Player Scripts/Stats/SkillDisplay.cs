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
            //player can forget the skill by clicking on it
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
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(true);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Enabled").gameObject.SetActive(false);
        }

        private void TurnOffSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(true);
            this.transform.Find("IconParent").Find("Enabled").gameObject.SetActive(false);
        }

        private void EnabledSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("IconParent").Find("Available").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Disabled").gameObject.SetActive(false);
            this.transform.Find("IconParent").Find("Enabled").gameObject.SetActive(true);

        }

        void ReactToChange()
        {
            EnableSkills();
        }

    }
}
