using UnityEngine;
using UnityEngine.UI;

public class PageScroller : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform pagesContainer;
    public int totalPages = 2;
    public float smoothTime = 0.2f; // Duration of smooth scrolling

    private int currentPage = 0;
    private float[] pagePositions;
    private float targetPosition;
    private float velocity = 0f;

    public Text titleText;

    void Start()
    {
        pagePositions = new float[totalPages];
        for (int i = 0; i < totalPages; i++)
        {
            pagePositions[i] = i / (float)(totalPages - 1);
        }

        targetPosition = pagePositions[currentPage];

        titleText.text = "SHORT LEVEL";
    }

    void Update()
    {
        // Smoothly interpolate to target position
        float newPosition = Mathf.SmoothDamp(scrollRect.horizontalNormalizedPosition, targetPosition, ref velocity, smoothTime);
        scrollRect.horizontalNormalizedPosition = newPosition;
    }

    public void NextPage()
    {
        if (currentPage < totalPages - 1)
        {
            currentPage++;
            SetTargetPosition();
        }
        titleText.text = "LONG LEVEL";
        
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            SetTargetPosition();
        }
        titleText.text = "SHORT LEVEL";
    }

    private void SetTargetPosition()
    {
        targetPosition = pagePositions[currentPage];
    }
}
