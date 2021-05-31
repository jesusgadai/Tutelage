using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mini Game", menuName = "Tutelage/Mini Game")]
public class GameEntryData : ScriptableObject
{
    public string title;
    public int price;
    public Sprite image;
    public Game game;
    public Status status = Status.LOCKED;
}

public enum Status
{
    LOCKED,
    UNLOCKED
}