using UnityEngine;

public class WorldShift : MonoBehaviour
{
    public GameObject worldA;
    public GameObject worldB;
    public GlitchEffect glitchEffect;
    public Rigidbody2D playerRb;

    [Header("World Settings")]
    public bool enableGravityFlip = false; // Toggle this in Inspector

    private bool inWorldA = true;

    void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        worldA.SetActive(true);
        worldB.SetActive(false);

        if (glitchEffect != null)
            glitchEffect.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            AudioManager.instance.Play("Whoos");
            ShiftWorlds();
        }
    }

    void ShiftWorlds()
    {
        // Detach player from platform to prevent them being deactivated
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.parent != null)
        {
            player.transform.SetParent(null);
        }

        inWorldA = !inWorldA;
        worldA.SetActive(inWorldA);
        worldB.SetActive(!inWorldA);

        if (!inWorldA && glitchEffect != null)
        {
            glitchEffect.enabled = true;
        }
        else
        {
            glitchEffect.enabled = false;
        }

        // Only flip gravity if enabled
        if (enableGravityFlip && playerRb != null)
        {
            playerRb.gravityScale *= -1;

            // Flip sprite visually
            Vector3 scale = playerRb.transform.localScale;
            scale.y *= -1;
            playerRb.transform.localScale = scale;
        }
    }
}
