using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public Sprite baseball;
    public Sprite bomb;

    Animator animator;
    SpriteRenderer spriteRenderer;
    AnimatorCallback animatorCallback;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animatorCallback = GetComponentInChildren<AnimatorCallback>();
        animatorCallback.onAnimationEnd.AddListener(AnimationEnd);
    }

    public void ThrowBall()
    {
        Vector3 scale = transform.localScale;
        int randomSide = Random.Range(1, 100);
        scale.x = randomSide > 50 ? 1 : -1;
        transform.localScale = scale;

        int randomSprite = Random.Range(1, 100);
        spriteRenderer.sprite = randomSprite > 75 ? bomb : baseball;

        int randomAnimation = Random.Range(1, 3);
        animator.speed = 2;
        animator.SetInteger("isThrown", randomAnimation);
    }

    void AnimationEnd()
    {
        Debug.Log("Animation end");
    }
}
