using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameOverManager.instance != null)
            {
                GameOverManager.instance.TriggerGameOver();
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                Debug.LogError("GameOverManager instance not found!");
            }
        }
    }
}
