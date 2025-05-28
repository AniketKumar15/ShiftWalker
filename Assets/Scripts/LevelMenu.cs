using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] btn;

    private void Awake()
    {
        int UnlockLevel = PlayerPrefs.GetInt("UnlockLevel", 1);
        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].interactable = false;
            // Safely find and hide the "Star" image
            Transform star = btn[i].transform.Find("Star");
            if (star != null)
                star.gameObject.SetActive(false);
        }
        for (int i = 0; i < UnlockLevel; i++)
        {
            btn[i].interactable = true;
            // Mark as completed (except last unlocked, which is current level)
            if (i < UnlockLevel - 1)
            {
                Transform star = btn[i].transform.Find("Star");
                if (star != null)
                    star.gameObject.SetActive(true);
            }
        }
    }
    public void OpenLevel1(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);

    }
    public void OpenLevel(int levelIndex)
    {
        string sceneName = "Level " + levelIndex;
        SceneManager.LoadScene(sceneName);
    }
}
