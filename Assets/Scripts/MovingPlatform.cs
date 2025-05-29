using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;
    private bool isFrozen = false;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Assign both pointA and pointB in the Inspector.");
            enabled = false;
            return;
        }

        target = pointB.position;
    }

    void Update()
    {
        if (isFrozen) return;

        // Move toward target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // If reached target, switch to the other point
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(transform, true);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Failed to parent player to platform: " + e.Message);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    public void FreezeForSeconds(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        yield return new WaitForSecondsRealtime(duration);
        isFrozen = false;
    }

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
