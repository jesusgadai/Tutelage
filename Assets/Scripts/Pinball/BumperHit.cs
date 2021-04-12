using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BumperHit : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent onBumperHit;

    public int scoreValue = 10;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (onBumperHit != null)
            onBumperHit.Invoke();

        animator.Play("Blink");
    }
}
