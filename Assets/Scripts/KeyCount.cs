using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyCount
{
    public string name;
    public int count;

    public KeyCount(string name, int count)
    {
        this.name = name;
        this.count = count;
    }
}
