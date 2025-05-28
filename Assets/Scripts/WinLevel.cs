using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    public GameObject gameWinPanel;
    private AudioSource bgMusic;

    private void Start()
    {

        GameObject bgMusicObj = GameObject.FindGameObjectWithTag("BGMUSIC");

        if (bgMusicObj != null)
        {
            bgMusic = bgMusicObj.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("BGMUSIC object not found. Background music will be skipped.");
        }
        // Try to find the GameOver panel if not set in Inspector
        if (gameWinPanel == null)
        {
            GameObject go = GameObject.Find("GameWin");
            if (go != null)
            {
                gameWinPanel = go;
            }
            else
            {
                Debug.LogError("GameWin panel not found. Please assign it in the Inspector.");
            }
        }

        // Ensure it starts hidden
        gameWinPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            gameWinPanel.SetActive(true);
            StartCoroutine(BgMusicControl());
            AudioManager.instance.Play("Win");
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    void UnlockNewLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int maxLevels = 8; // Total number of levels (should match your button array size)

        if (currentLevel >= PlayerPrefs.GetInt("ReachedLevel"))
        {
            PlayerPrefs.SetInt("ReachedLevel", currentLevel + 1);

            int nextUnlock = PlayerPrefs.GetInt("UnlockLevel", 1) + 1;
            nextUnlock = Mathf.Clamp(nextUnlock, 1, maxLevels); //  Clamp so it doesn't exceed
            PlayerPrefs.SetInt("UnlockLevel", nextUnlock);

            PlayerPrefs.Save();
        }
    }

    IEnumerator BgMusicControl()
    {
        bgMusic.volume = 0f;
        yield return new WaitForSeconds(1f);
        bgMusic.volume = 1f;
    }
}
