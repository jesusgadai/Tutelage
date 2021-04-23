using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorizontalPad : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent onTriggerEnter;

    public int scoreValue = 10;

    SpriteRenderer spriteRenderer;
    bool active = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!active)
        {
            if (onTriggerEnter != null)
                onTriggerEnter.Invoke();

            ChangeSpriteRendererAlpha(0.75f);
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (active)
        {
            ChangeSpriteRendererAlpha(1f);
            active = false;
        }
    }

    void ChangeSpriteRendererAlpha(float alphaValue)
    {
        Color color = spriteRenderer.color;
        color.a = alphaValue;
        spriteRenderer.color = color;
    }
}
