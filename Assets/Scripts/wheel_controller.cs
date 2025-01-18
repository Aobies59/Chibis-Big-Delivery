using UnityEngine;

public class wheel_controller : MonoBehaviour
{

    private float turnSpeed = 30f;
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
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, Mathf.Max(-maxTurnAngle, currentY - (turnSpeed * deltaTime)), currentRotation.eulerAngles.z);
            }
            else {
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentY - (turnSpeed * deltaTime), currentRotation.eulerAngles.z);
            }
        }
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
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, Mathf.Min(maxTurnAngle, currentY + (turnSpeed * deltatime)), currentRotation.eulerAngles.z);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentY + (turnSpeed * deltatime), currentRotation.eulerAngles.z);
            }
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
