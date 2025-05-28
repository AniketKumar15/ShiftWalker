using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ClampToCamera : MonoBehaviour
{
    public Camera mainCamera;
    private Rigidbody2D rb;

    private float halfWidth;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        rb = GetComponent<Rigidbody2D>();

        // Calculate half the width of the player using SpriteRenderer
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.size.x / 2f;
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = rb.position;

        // Get camera bounds in world space (only x-axis is relevant)
        Vector3 min = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 max = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Clamp only on the x-axis
        pos.x = Mathf.Clamp(pos.x, min.x + halfWidth, max.x - halfWidth);

        // Y-axis is left untouched
        rb.position = pos;
    }
}
