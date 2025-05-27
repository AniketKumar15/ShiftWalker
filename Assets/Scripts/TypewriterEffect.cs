//using System.Collections;
//using UnityEngine;
//using TMPro; // Import TextMeshPro
//using UnityEngine.SceneManagement;

//public class TypewriterEffect : MonoBehaviour
//{
//    public TMP_Text dialogueText; // Assign this in the Inspector
//    public float typingSpeed = 0.05f; // Speed of typing
//    public float linePause = 1.5f; // Pause after each line

//    private string[] dialogueLines = {
//        "Hello, Player. It's me, Koko.",
//        "Listen carefully—I have a secret to tell you.",
//        "The developer gives hints throughout the game...",
//        "But there's a catch—every hint is a lie.",
//        "They are meant to trick you.",
//        "If you want to pass the levels, do the opposite of what the hints say.",
//        "Oh, and one more thing...",
//        "Don't tell anyone I told you this.",
//        "It's our little secret."
//    };

//    void Start()
//    {
//        // Check if the cutscene has been watched before
//        if (PlayerPrefs.GetInt("CutsceneWatched", 0) == 1)
//        {
//            SkipCutscene();
//            return;
//        }

//        dialogueText.text = ""; // Clear text at start
//        StartCoroutine(DisplayDialogue());
//    }

//    IEnumerator DisplayDialogue()
//    {
//        foreach (string line in dialogueLines)
//        {
//            yield return StartCoroutine(TypeLine(line));
//            yield return new WaitForSeconds(linePause); // Pause before next line
//            dialogueText.text = ""; // Clear text before new line

//        }
//        // Mark cutscene as watched and load the next scene
//        PlayerPrefs.SetInt("CutsceneWatched", 1);
//        PlayerPrefs.Save();

//        LoadNextScene();
//    }

//    IEnumerator TypeLine(string line)
//    {
//        dialogueText.text = ""; // Clear text before typing
//        foreach (char letter in line.ToCharArray())
//        {
//            dialogueText.text += letter; // Add letter one by one
//            AudioManager.instance.Play("typeSound");
//            yield return new WaitForSeconds(typingSpeed); // Wait before next letter
//        }
//    }
//    void SkipCutscene()
//    {
//        LoadNextScene(); // Directly load the next level
//    }

//    void LoadNextScene()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//    }
//}
using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro
using UnityEngine.SceneManagement;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text dialogueText; // Assign this in the Inspector
    public float typingSpeed = 0.05f; // Speed of typing
    public float linePause = 1.5f; // Pause after each line
    public bool isFinalCutscene = false; // Check if it's the final cutscene

    public string[] introDialogue = {
        "Hello, Player. It's me, Koko.",
        "Listen carefully—I have a secret to tell you.",
        "The developer gives hints throughout the game...",
        "But there's a catch—every hint is a lie.",
        "They are meant to trick you.",
        "If you want to pass the levels, do the opposite of what the hints say.",
        "Oh, and one more thing...",
        "Don't tell anyone I told you this.",
        "It's our little secret."
    };

    public string[] finalDialogue = {
        "So, you made it to the end...",
        "You really thought I was guiding you?",
        "...You poor thing.",
        "Every hint you followed...",
        "Every wrong answer you corrected...",
        "It was all *me*.",
        "I was the one who set the wrong hints.",
        "You never outsmarted the game...",
        "You only followed *my* rules.",
        "Funny, isn't it?",
        "We look for hints, believing they’ll lead us to the truth...",
        "But what if every hint was a lie from the start?",
        "What if the right path never existed at all?"
    };

    void Start()
    {
        if (PlayerPrefs.GetInt("CutsceneWatched", 0) == 1 && !isFinalCutscene)
        {
            SkipCutscene();
            return;
        }

        dialogueText.text = ""; // Clear text at start
        StartCoroutine(DisplayDialogue());
    }

    IEnumerator DisplayDialogue()
    {
        string[] dialogueLines = isFinalCutscene ? finalDialogue : introDialogue;

        foreach (string line in dialogueLines)
        {
            yield return StartCoroutine(TypeLine(line));

            if (isFinalCutscene)
            {
                yield return new WaitForSeconds(2f); // Longer pauses for drama
            }
            else
            {
                yield return new WaitForSeconds(linePause);
            }

            dialogueText.text = "";
        }

        if (!isFinalCutscene)
        {
            PlayerPrefs.SetInt("CutsceneWatched", 1);
            PlayerPrefs.Save();
        }

        LoadNextScene();
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            AudioManager.instance.Play("typeSound");

            if (isFinalCutscene && Random.value > 0.9f) // Random glitch effect
            {
                dialogueText.text = "<color=red>" + dialogueText.text + "</color>";
                yield return new WaitForSeconds(0.1f);
                dialogueText.text = dialogueText.text.Replace("<color=red>", "").Replace("</color>", "");
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void SkipCutscene()
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (!isFinalCutscene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
