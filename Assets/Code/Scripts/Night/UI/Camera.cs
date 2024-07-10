using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float t;
    [SerializeField] float speed;

    private Vector3 velocity;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GameManager.gameManager.player.transform.position, ref velocity, t, speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -15);
    }
}
