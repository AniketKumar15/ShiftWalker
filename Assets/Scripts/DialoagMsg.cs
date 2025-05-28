using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro
using UnityEngine.SceneManagement;

public class DialoagMsg : MonoBehaviour
{
    public TMP_Text dialogueText; // Assign this in the Inspector
    public float typingSpeed = 0.05f; // Speed of typing
    public float linePause = 1.5f; // Pause after each line
    public bool isFinalCutscene = false; // Check if it's the final cutscene

    public string[] introDialogue = {
        "Hello, Player. It's me, Koko.",
        "Listen carefully — I have a secret to tell you.",
        "I'm giving you the power to shift between two worlds.",
        "You’re probably wondering why.",
        "It's because I want a powerful diamond.",
        "But unfortunately, I can’t get it on my own...",
        "So, I need your help to find it.",
        "One more thing — remember this well:",
        "Changing worlds can shift platform positions, gravity, and many other things.",
        "Use this power wisely."
    };

    public string[] finalDialogue = {
        "Ah... so you finally found the diamond.",
        "I knew you would. You always had potential.",
        "But here's the thing... I don't need it anymore.",
        "What I need now... is <i>you</i>.",
        "You see, the power I gave you — it was never a gift.",
        "It was a test.",
        "And you passed.",
        "Congratulations, Player... you're the perfect subject.",
        "From now on, <b>your choices</b> belong to me.",
        "Let the real experiment begin."
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
    public void StartDialogue()
    {
        StopAllCoroutines();
        dialogueText.text = "";
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
