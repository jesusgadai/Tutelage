using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mini Game", menuName = "Tutelage/Mini Game")]
public class MiniGame : ScriptableObject
{
    public string title;
    public int price;
    public Sprite image;
    public GameCategory gameCategory;
    public Status status = Status.LOCKED;
}

public enum Status
{
    LOCKED,
    UNLOCKED
}