using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sprite icon;
    public bool readingGame;
}