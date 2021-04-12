using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public bool isKeyPress = false;
    public float speed = 0f;
    private HingeJoint2D hingeJoint2D;
    private JointMotor2D jointMotor;

    // Start is called before the first frame update
    void Awake()
    {
        hingeJoint2D = GetComponent<HingeJoint2D>();
        jointMotor = hingeJoint2D.motor;
    }

    void FixedUpdate()
    {
        // on press keyboard or touch Screen
        if (isKeyPress == true)
        {
            // set the motor speed to max
            jointMotor.motorSpeed = speed;
            hingeJoint2D.motor = jointMotor;
        }
        else
        {
            // snap the motor back again
            jointMotor.motorSpeed = -speed;
            hingeJoint2D.motor = jointMotor;
        }
    }

    public void ButtonPressed()
    {
        isKeyPress = true;
    }

    public void ButtonReleased()
    {
        isKeyPress = false;
    }
}
