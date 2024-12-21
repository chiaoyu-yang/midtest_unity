using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Vector2 targetPosition;
    private float moveDistance = 10f;
    private float moveSpeed = 40f;

    private Coroutine moveCoroutine;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            targetPosition = originalPosition + new Vector2(moveDistance, 0);
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(SmoothMove(rectTransform.anchoredPosition, targetPosition));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            targetPosition = originalPosition;
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(SmoothMove(rectTransform.anchoredPosition, targetPosition));
        }
    }

    private IEnumerator SmoothMove(Vector2 start, Vector2 end)
    {
        float elapsedTime = 0f;
        float duration = Vector2.Distance(start, end) / moveSpeed;

        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = end;
    }
}