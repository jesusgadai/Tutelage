using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorCallback : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent onAnimationEnd;
    public void AnimationEnd()
    {
        if (onAnimationEnd != null)
            onAnimationEnd.Invoke();
    }
}
