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