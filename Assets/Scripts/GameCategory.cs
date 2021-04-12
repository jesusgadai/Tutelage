using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameCategory
{
    public Sprite banner;
    public Sprite icon;
    public string title;
    public string description;
    [EnumFlagsAttribute]
    public RequirementsEnum requirements;
    public List<Game> games = new List<Game>();
}

[System.Serializable]
public class Game
{
    public string title;
    public string mechanics;
    public int tokensToEarn;
    public Sprite banner;
    public Sprite smallBanner;
    [EnumFlagsAttribute]
    public DevSkillsEnum devSkills;
    public bool readingGame;

    public Game nextGame;
    [HideInInspector]
    public string categoryTitle;
    [HideInInspector]
    public Sprite categoryIcon;
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