using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameCategory
{
    public Sprite banner;
    public string title;
    public string description;
    [EnumFlagsAttribute]
    public RequirementsEnum requirements;
    public List<Game> games = new List<Game>();
}

[System.Serializable]
public class Game
{
    public Sprite banner;
    public Sprite smallBanner;
    public string title;
    public int tokensToEarn;
    public string mechanics;
    [EnumFlagsAttribute]
    public DevSkillsEnum devSkills;
}

[System.Flags]
public enum RequirementsEnum
{
    parentsPositiveAffirmations,
    friends
}

public static class Requirements
{
    public static string EnumToString(RequirementsEnum reqEnum)
    {
        switch (reqEnum)
        {
            case RequirementsEnum.parentsPositiveAffirmations:
                return "Parent's Positive Affirmations";
            case RequirementsEnum.friends:
                return "Friends";
        }
        return null;
    }
}