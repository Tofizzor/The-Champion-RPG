using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    //[CreateAssetMenu(menuName = "Player/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public int LevelNeeded;
        public int XPNeeded;

        public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();

        [Header("Battle Stats")]
        //name of the attack
        public string skillType;
        public string skillName;
        public float skillCost;


        //Method to set the values in the Skills UI
        public void SetValues(GameObject SkillDisplayObject, PlayerStats Player)
        {

            //check is SO is used
            if (SkillDisplayObject)
            {
                SkillDisplay SD = SkillDisplayObject.GetComponent<SkillDisplay>();
                SD.skillName.text = name;
                if (SD.skillDescription)
                    SD.skillDescription.text = Description;

                if (SD.skillIcon)
                    SD.skillIcon.sprite = Icon;

                if (SD.skillLevel)
                    SD.skillLevel.text = LevelNeeded.ToString();

                if (SD.skillXPNeeded)
                    SD.skillXPNeeded.text = XPNeeded.ToString() + "XP";

                if (SD.skillAttribute)
                    SD.skillAttribute.text = AffectedAttributes[0].attribute.ToString();

                if (SD.skillAttrAmount)
                    SD.skillAttrAmount.text = "+" + AffectedAttributes[0].amount.ToString();

            }
        }



        //check is player is able to get the skill
        public bool CheckSkills(PlayerStats Player)
            {
                //checks if player has needed level
                if (Player.PlayerLevel < LevelNeeded)
                    return false;
                //check if player has enough xp
                if (Player.PlayerXP < XPNeeded)
                    return false;

                //if all the requirements are met allow player to learn the skill
                return true;
            }
            
            //if skill is not learned, allow player to learn it
            public bool EnableSkill(PlayerStats Player)
            {
                //check already learned skills array
                List<Skills>.Enumerator skills = Player.PlayerSkills.GetEnumerator();
                //while there is skills that player already learned, move to next one
                while (skills.MoveNext())
                {
                var CurrSkill = skills.Current;
                if (CurrSkill.name == this.name)
                {
                    return false;
                }

                }
                return true;
            }


        //learn new skill
        public bool GetSkill(PlayerStats Player)
        {
            int i = AffectedAttributes.Count;
            //get all the attributes
            List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
            while (attributes.MoveNext())
            {
                //List throught the players attributes and match with skill attribute
                List<PlayerAttributes>.Enumerator PlayerAttr = Player.Attributes.GetEnumerator();
                while (PlayerAttr.MoveNext())
                {

                    if (attributes.Current.attribute.name.ToString() == PlayerAttr.Current.attribute.name.ToString())
                    {
                        //update the players attributes
                        PlayerAttr.Current.amount += attributes.Current.amount;
                        //mark that attribute was updated
                        i--;
                        break;
                    }
                }
                if (i == 0)
                {
                    //take the XP from player
                    Player.PlayerXP -= this.XPNeeded;
                    //add skill to the list
                    Player.PlayerSkills.Add(this);
                    return true;
                }
            }
            return false;

        }

    }
}
