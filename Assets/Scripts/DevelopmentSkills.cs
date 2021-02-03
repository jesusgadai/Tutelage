using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DevelopmentSkills : MonoBehaviour
{
    public TMP_Text title;
    public Image image;

    public void SetSkill(DevelopmentSkillObject devObject)
    {
        this.title.text = devObject.title;
        this.image.sprite = devObject.image;
    }
}

[System.Flags]
public enum DevSkillsEnum
{
    selfControlStrategies, characterBuilding, communicationSkills, resolvingConflict, confidence, integrity, empathyForPeers, hygiene, playSkills, problemSolving, developingFriendship, selfLove
}

public static class DevSkills
{
    public static string EnumToString(DevSkillsEnum devSkillsEnum)
    {
        switch (devSkillsEnum)
        {
            case DevSkillsEnum.selfControlStrategies:
                return "Self-Control Strategies";
            case DevSkillsEnum.characterBuilding:
                return "Character Building";
            case DevSkillsEnum.communicationSkills:
                return "Communication Skills";
            case DevSkillsEnum.resolvingConflict:
                return "Resolving Conflict";
            case DevSkillsEnum.confidence:
                return "Confidence";
            case DevSkillsEnum.integrity:
                return "Integrity";
            case DevSkillsEnum.empathyForPeers:
                return "Empathy For Peers";
            case DevSkillsEnum.hygiene:
                return "Hygiene";
            case DevSkillsEnum.playSkills:
                return "Play Skills";
            case DevSkillsEnum.problemSolving:
                return "Problem Solving";
            case DevSkillsEnum.developingFriendship:
                return "Developing Friendship";
            case DevSkillsEnum.selfLove:
                return "Self-Love";
        }
        return null;
    }
}