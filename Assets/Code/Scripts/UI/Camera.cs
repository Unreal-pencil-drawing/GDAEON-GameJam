using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float t;
    [SerializeField] float speed;

    private Vector3 velocity;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, t, speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -15);
    }
}
