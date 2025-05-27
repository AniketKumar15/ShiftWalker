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
        }
        for (int i = 0; i < UnlockLevel; i++)
        {
            btn[i].interactable = true;
        }
    }

    public void OpenLevel(int levelIndex)
    {
        string sceneName = "Level " + levelIndex;
        SceneManager.LoadScene(sceneName);
    }
}
