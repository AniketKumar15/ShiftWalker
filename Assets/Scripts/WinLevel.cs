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
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedLevel"))
        {
            PlayerPrefs.SetInt("ReachedLevel", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("UnlockLevel", 1) + 1);
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
