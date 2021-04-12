using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hole : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent onBalLFell;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Ball"))
            if (onBalLFell != null)
                onBalLFell.Invoke();
    }
}
