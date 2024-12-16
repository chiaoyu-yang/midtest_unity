using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffectTop : MonoBehaviour
{
    private RectTransform rectTransform; // UI 按鈕的 RectTransform
    private Vector2 originalPosition;    // 按鈕的原始位置
    private Vector2 targetPosition;      // 按鈕的目標位置
    private float moveDistance = 10f;    // 滑鼠懸停時向上的位移量 (單位: 像素)
    private float moveSpeed = 40f;       // 移動速度

    private Coroutine moveCoroutine;     // 用於控制移動的 Coroutine

    void Start()
    {
        // 獲取 RectTransform 並初始化原始位置
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
        else
        {
            Debug.LogError("2 RectTransform is missing on this UI element!");
        }
    }

    public void OnMouseEnter1()
    {
        Debug.Log("Mouse entered help_btn");
        if (rectTransform != null)
        {
            targetPosition = originalPosition + new Vector2(0, moveDistance);
            // 停止當前移動的 Coroutine 並啟動新的
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(SmoothMove(rectTransform.anchoredPosition, targetPosition));
        }
    }

    public void OnMouseExit1()
    {
        if (rectTransform != null)
        {
            targetPosition = originalPosition;
            // 停止當前移動的 Coroutine 並啟動新的
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(SmoothMove(rectTransform.anchoredPosition, targetPosition));
        }
    }

    private IEnumerator SmoothMove(Vector2 start, Vector2 end)
    {
        float elapsedTime = 0f;
        float duration = Vector2.Distance(start, end) / moveSpeed; // 根據距離和速度計算移動時間

        while (elapsedTime < duration)
        {
            // 線性插值位置
            rectTransform.anchoredPosition = Vector2.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 確保最後的位置正確
        rectTransform.anchoredPosition = end;
    }
}
