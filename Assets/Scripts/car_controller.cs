using System;
using UnityEngine;

public class car_controller : MonoBehaviour
{
    public float speed = 0.0f;
    public float maxSpeed = 100.0f;
    public float maxReverseSpeed = -30.0f;
    public float acceleration = 10.0f;
    public float decelerationFactor = 2f;
    public float brakingForce = 20.0f;

    private float inputAxis;

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
        inputAxis = Input.GetAxis("Vertical");
        float movementDirection = Input.GetAxis("Vertical");

        UpdateSpeed();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        UpdateWheels();

    }

    void UpdateSpeed() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyBraking();
            return;
        }
        if (inputAxis != 0) {
            if (inputAxis > 0 && speed < 0) {
                ApplyBraking();
                return;
            } else if (inputAxis < 0 && speed > 0) {
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

            speed += inputAxis * acceleration * accelerationMultiplier * Time.deltaTime;

            speed = Mathf.Clamp(speed, maxReverseSpeed, maxSpeed);
        } else {
            speed = Mathf.MoveTowards(speed, 0, decelerationFactor * Time.deltaTime);
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
        float turnDirection = Input.GetAxis("Horizontal");

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
