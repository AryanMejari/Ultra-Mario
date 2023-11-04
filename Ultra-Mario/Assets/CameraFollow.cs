using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The player object to follow
    public float smoothSpeed = 0.125f;  // The smoothness of the camera follow

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
