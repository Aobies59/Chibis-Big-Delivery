using System;
using Unity.VisualScripting;
using UnityEngine;

public class car_controller : MonoBehaviour
{
    public float speed = 0.0f;
    public const float MaxPossibleSpeed = 100.0f;
    private float maxSpeed = 100.0f;
    public float maxReverseSpeed = -30.0f;
    public const float MaxTurningSpeed = 80.0f;
    public const float MaxPossibleReverseSpeed = -30.0f;
    public const float MaxReverseTurningSpeed = -20f;
    public float acceleration = 10.0f;
    const float DecelerationFactor = 2f;
    public float brakingForce = 20.0f;

    private float movementDirection;
    private float turnDirection;

    private bool isTurning = false;

    public GameObject frontLeftWheel;
    private wheel_controller frontLeftWheelController;
    public GameObject frontRightWheel;
    private wheel_controller frontRightWheelController;

    void Start()
    {
        frontLeftWheelController = frontLeftWheel.GetComponent<wheel_controller>();
        frontRightWheelController = frontRightWheel.GetComponent<wheel_controller>();        
    }

    void Update()
    {
        movementDirection = Input.GetAxis("Vertical");

        UpdateSpeed();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        UpdateWheels();
    }

    void UpdateSpeed() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyBraking();
            return;
        }
        if (movementDirection != 0) {
            if (movementDirection > 0 && speed < 0) {
                ApplyBraking();
                return;
            } else if (movementDirection < 0 && speed > 0) {
                ApplyBraking();
                return;
            }

            float accelerationMultiplier;
            if (speed < 60f) {
                accelerationMultiplier = 1f;
            } else if (speed < 80f) {
                accelerationMultiplier = 0.5f;
            } else {
                accelerationMultiplier = 0.2f;
            }

            speed += movementDirection * acceleration * accelerationMultiplier * Time.deltaTime;
            if (isTurning) {
                maxSpeed = Mathf.MoveTowards(MaxTurningSpeed, 0, 2f * Time.deltaTime);
                maxReverseSpeed = Mathf.MoveTowards(MaxReverseTurningSpeed, 0, 2f * Time.deltaTime);
            } else {
                maxSpeed = MaxPossibleSpeed;
                maxReverseSpeed = MaxPossibleReverseSpeed;
            }

            speed = Mathf.Clamp(speed, maxReverseSpeed, maxSpeed);

        } else {
            speed = Mathf.MoveTowards(speed, 0, DecelerationFactor * Time.deltaTime);
        }
    }

    void ApplyBraking() {
        if (speed > 0) {
            speed -= brakingForce * Time.deltaTime;
        } else if (speed < 0) {
            speed += brakingForce * Time.deltaTime;
        }

        if (Mathf.Abs(speed) < 0.1f) {
            speed = 0;
        }
    }

    void UpdateWheels() {

        turnDirection = Input.GetAxis("Horizontal");

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
                isTurning = true;
                if (speed > 0) {
                    transform.Rotate(0, rotationSpeedFactor * wheelYAngle * Time.deltaTime, 0);
                } else {
                    transform.Rotate(0, -rotationSpeedFactor * wheelYAngle * Time.deltaTime, 0);
                }
            } else {
                isTurning = false;
            }
        }
    }
}
