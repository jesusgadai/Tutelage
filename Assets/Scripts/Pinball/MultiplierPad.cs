using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiplierPad : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent onTriggerEnter;
    [HideInInspector]
    public UnityEvent onMultiplierExpire;

    public int multiplierValue;
    public float duration = 3f;

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

            StartCoroutine(MultiplierExpire());

            ChangeSpriteRendererAlpha(0.75f);
            active = true;
        }
    }

    IEnumerator MultiplierExpire()
    {
        yield return new WaitForSeconds(duration);

        if (onMultiplierExpire != null)
            onMultiplierExpire.Invoke();

        ChangeSpriteRendererAlpha(1f);
        active = false;
    }

    void ChangeSpriteRendererAlpha(float alphaValue)
    {
        Color color = spriteRenderer.color;
        color.a = alphaValue;
        spriteRenderer.color = color;
    }
}
