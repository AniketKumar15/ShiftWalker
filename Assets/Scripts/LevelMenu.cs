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
            bool isFinalLevel = (i == btn.Length - 1);
            bool shouldShowStar = (i < UnlockLevel - 1 || isFinalLevel); // updated condition

            if (shouldShowStar)
            {
                Transform star = btn[i].transform.Find("Star");
                if (star != null)
                    star.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) // Press "U" to unlock all levels
        {
            PlayerPrefs.SetInt("UnlockLevel", 8); // Unlock up to Level 8
            PlayerPrefs.Save();
            Debug.Log("All levels unlocked for testing.");
        }
        if (Input.GetKey(KeyCode.P) && Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs reset.");
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
