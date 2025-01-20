using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float positionSpeed = 10f;
    public float rotationSpeed = 100f;

    public Vector3 offset;
    public float maxDistance = 10f;

    Vector3 previousTargetPosition;

    void Start()
    {
        previousTargetPosition = target.position;
    }

    void LateUpdate()
    {   
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, positionSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, desiredPosition) > maxDistance) {
            Vector3 offset = desiredPosition - previousTargetPosition;
            transform.position += offset;
        } else {
            transform.position = smoothedPosition;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);

        previousTargetPosition = target.position;
    }
}
