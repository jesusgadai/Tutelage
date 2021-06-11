using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book", menuName = "Tutelage/Book")]
public class Book : ScriptableObject
{
    public string title;
    public string summary;
    public string story;
    public Sprite cover;
    public Status status = Status.LOCKED;
}
