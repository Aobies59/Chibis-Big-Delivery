using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public bool isSelected = false;
    public bool isActive = true;

    float moveSpeed = 4.0f;
    Vector3 initialPosition;
    Quaternion initialRotation;

    void Start()
    {
        // TEMP
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Reset() {
        if (GetComponent<Rigidbody>()) {
            Destroy(GetComponent<Rigidbody>());
        }
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        isActive = true;
    }

    void Update()
    {
        if (!isSelected) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }

        if (!isActive) {
            return;
        }

        float xAxis = -Input.GetAxis("Horizontal");
        float zAxis = -Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xAxis, 0, zAxis) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space)) {
            gameObject.AddComponent<Rigidbody>();
            isActive = false;
        }
    }
}
