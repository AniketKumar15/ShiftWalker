using UnityEngine;
using System.Collections;

public class TimeFreeze : MonoBehaviour
{
    public float freezeDuration = 2f;
    private MovingPlatform[] platforms;

    void Start()
    {
        // Find all platforms in the scene that can be frozen
        platforms = FindObjectsOfType<MovingPlatform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            FreezeTime();
        }
    }

    public void FreezeTime()
    {
        StartCoroutine(FreezeRoutine());
    }

    IEnumerator FreezeRoutine()
    {
        Debug.Log("FREEZE TIME");

        foreach (var platform in platforms)
        {
            platform.FreezeForSeconds(freezeDuration);
        }

        yield return new WaitForSecondsRealtime(freezeDuration);

        Debug.Log("TIME NORMAL");
    }
}
