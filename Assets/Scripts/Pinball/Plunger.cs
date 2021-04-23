using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    public GameObject ball;
    public float xForce;
    public float yForce;

    Rigidbody2D ballRigidBody;
    bool hasBall = true;

    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        if (hasBall)
        {
            ballRigidBody.simulated = true;
            ballRigidBody.AddForce(new Vector2(xForce, yForce));

            hasBall = false;
        }
    }

    public void ResetBall()
    {
        ballRigidBody.simulated = false;
        ballRigidBody.velocity = Vector2.zero;
        ball.transform.position = transform.position;
        ball.transform.rotation = Quaternion.identity;
        hasBall = true;
    }
}
