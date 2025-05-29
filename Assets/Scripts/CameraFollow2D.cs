using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;                     // Player transform
    public Vector2 offset = new Vector2(0f, -2f); // Offset from player
    public float smoothSpeed = 5f;               // Smooth follow speed

    private Collider2D playerCollider;

    void Start()
    {
        if (target != null)
        {
            playerCollider = target.GetComponent<Collider2D>();
        }
    }

    void LateUpdate()
    {
        if (target == null || playerCollider == null) return;

        if (!playerCollider.enabled) return; // Stop following if player is "dead"

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z); // Keep camera's Z position

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
