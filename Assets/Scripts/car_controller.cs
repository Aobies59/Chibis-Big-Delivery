using System;
using UnityEngine;

public class car_controller : MonoBehaviour
{
    public float speed = 0.0f;
    const float maxPossibleSpeed = 100.0f;
    public float maxSpeed = 100.0f;
    public float maxTurningSpeed = 50.0f;
    public float maxReverseSpeed = -30.0f;
    public float acceleration = 10.0f;
    public float deceleration = 30.0f;
    public float turnSpeed = 50.0f;

    // wheels objects
    public GameObject frontLeftWheel;
    private wheel_controller frontLeftWheelController;
    public GameObject frontRightWheel;
    private wheel_controller frontRightWheelController;
    public GameObject backLeftWheel;
    public GameObject backRightWheel;

    void Start()
    {
        frontLeftWheelController = frontLeftWheel.GetComponent<wheel_controller>();
        frontRightWheelController = frontRightWheel.GetComponent<wheel_controller>();        
    }

    void Update()
    {
        float movementDirection = Input.GetAxis("Vertical");

        float targetSpeed = 0;
        if (movementDirection > 0) {
            targetSpeed = maxSpeed;
        } else if (movementDirection < 0) {
            targetSpeed = maxReverseSpeed;
        }

        float accelerationFactor;
        if (speed < targetSpeed) {
            if (speed > 0) {
                accelerationFactor = deceleration;
            } else {
                accelerationFactor = acceleration;
            }
        } else {
            accelerationFactor = acceleration;
        }

        float speedDifference = targetSpeed - speed;
        float easing = Mathf.Sign(speedDifference) * Mathf.Pow(Math.Abs(speedDifference), 0.5f);
        speed += easing * accelerationFactor * Time.deltaTime;

        if (movementDirection == 0) {
            speed = Mathf.MoveTowards(speed, 0, deceleration * Time.deltaTime);
        } else {
            speed = Mathf.Clamp(speed, maxReverseSpeed, maxSpeed);
        }

        transform.Translate(0, 0, speed * Time.deltaTime);

        float turnDirection = Input.GetAxis("Horizontal");
        
        if (turnDirection == 0) {
            maxSpeed = maxPossibleSpeed;
        } else {
            maxSpeed = maxTurningSpeed;
        }
        
        if (turnDirection < 0) {
            frontLeftWheelController.TurnLeft(Time.deltaTime);
            frontRightWheelController.TurnLeft(Time.deltaTime);
        } else if (turnDirection > 0) {
            frontLeftWheelController.TurnRight(Time.deltaTime);
            frontRightWheelController.TurnRight(Time.deltaTime);
        }

        float rotationSpeedFactor = 1.5f;

        if (speed != 0) {
            float wheelYAngle = frontLeftWheel.transform.localRotation.eulerAngles.y;
            if (wheelYAngle > 180) {
                wheelYAngle -= 360;
            }
            if (wheelYAngle != 0) {
                if (speed > 0) {
                    transform.Rotate(0, rotationSpeedFactor * wheelYAngle * Time.deltaTime, 0);
                } else {
                    transform.Rotate(0, -rotationSpeedFactor * wheelYAngle * Time.deltaTime, 0);
                }
            }
        }
    }
}
