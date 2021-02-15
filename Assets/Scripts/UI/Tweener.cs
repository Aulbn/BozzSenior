using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    public static void Jump(MonoBehaviour owner, RectTransform rectTransform, float height, float transitionTime, Action onTop, Action onDone)
    {
        owner.StartCoroutine(IEJump(rectTransform, height, transitionTime, onTop, onDone));
    }
    public static void Jump(MonoBehaviour owner, RectTransform rectTransform, float height, float transitionTime, Action onDone)
    {
        Jump(owner, rectTransform, height, transitionTime, () => { }, onDone);
    }
    private static IEnumerator IEJump(RectTransform rectTransform, float height, float transitionTime, Action onTop, Action onDone)
    {
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 highestPos = startPos + new Vector2(0, height * 2);
        float time = 0;
        bool halfway = false;

        while (time < transitionTime)
        {
            time += Time.deltaTime;

            if (!halfway && time / transitionTime > 0.5f)
                onTop();
            halfway = time / transitionTime > 0.5f;

            rectTransform.anchoredPosition = Vector2.Lerp(halfway ? highestPos : startPos, halfway ? startPos : highestPos, time / transitionTime);
            yield return new WaitForEndOfFrame();
        }
        onDone();
    }

    public static float InvertFloat(float value, float max)
    {
        return max - value;
    }
}
