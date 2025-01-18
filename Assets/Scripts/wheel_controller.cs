using System;
using UnityEngine;

public class wheel_controller : MonoBehaviour
{

    private float turnSpeed = 50f;
    private float maxTurnAngle = 50f;

    public void TurnLeft(float deltaTime)
    {

        Quaternion currentRotation = transform.localRotation;
        float currentY = currentRotation.eulerAngles.y;
        if (currentY > 180) {
            currentY -= 360;
        }

        if (currentY > -maxTurnAngle)
        {
            if (currentY <= 0)
            {
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentY - (turnSpeed * deltaTime), currentRotation.eulerAngles.z);
            }
            else {
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentY - (turnSpeed * deltaTime), currentRotation.eulerAngles.z);
            }
        }

        ClampRotation();
    }

    public void TurnRight(float deltatime) {
        Quaternion currentRotation = transform.localRotation;
        float currentY = currentRotation.eulerAngles.y;
        if (currentY > 180) {
            currentY -= 360;
        }

        if (currentY < maxTurnAngle)
        {
            if (currentY >= 0)
            {
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentY + (turnSpeed * deltatime), currentRotation.eulerAngles.z);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentY + (turnSpeed * deltatime), currentRotation.eulerAngles.z);
            }
        }

        ClampRotation();
    }

    private void ClampRotation() {
        Quaternion currentRotation = transform.localRotation;
        // clamp the rotation to the max turn angle
        float currentY = currentRotation.eulerAngles.y;
        if (currentY > 180) {
            currentY -= 360;
        }

        if (currentY > maxTurnAngle) {
            transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, maxTurnAngle, currentRotation.eulerAngles.z);
        } else if (currentY < -maxTurnAngle) {
            transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, -maxTurnAngle, currentRotation.eulerAngles.z);
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
