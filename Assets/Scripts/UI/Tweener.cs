using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tweener : MonoBehaviour
{
    //Jump
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

    //Pulse scale
    public static void Pulse(MonoBehaviour owner, RectTransform rectTransform, float scaleMultiplier, float transitionTime, Action onDone)
    {
        owner.StartCoroutine(IEPulse(rectTransform, scaleMultiplier, transitionTime, onDone));
    }
    private static IEnumerator IEPulse(RectTransform rectTransform, float scaleMultiplier, float transitionTime, Action onDone)
    {
        Vector2 startScale = rectTransform.localScale;
        Vector2 biggestScale = startScale * scaleMultiplier;
        float time = 0;
        bool halfway = false;

        while (time < transitionTime)
        {
            time += Time.deltaTime;

            halfway = time / transitionTime > 0.5f;

            rectTransform.localScale = Vector2.Lerp(halfway ? biggestScale : startScale, halfway ? startScale : biggestScale, time / transitionTime);
            yield return new WaitForEndOfFrame();
        }
        onDone();
    }

    //CrossFadeColor
    public static void CrossFadeColor(MonoBehaviour owner, Image image, Color color, float time, Action onDone)
    {
        owner.StartCoroutine(IECrossFadeColor(image, color, time, onDone));
    }
    private static IEnumerator IECrossFadeColor(Image image, Color color, float time, Action onDone)
    {
        float timer = 0;
        Color startColor = image.color;

        while (timer < time)
        {
            timer += Time.unscaledDeltaTime;
            image.color = Color.Lerp(startColor, color, time / timer);
            yield return new WaitForEndOfFrame();
        }
        image.color = color;
        onDone();
    }

    //Timed Action
    public static void TimedAction(MonoBehaviour owner, float waitTime, Action onDone)
    {
        owner.StartCoroutine(IETimedAction(waitTime, onDone));
    }
    private static IEnumerator IETimedAction(float waitTime, Action onDone)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        onDone();
    }


    //Misc
    public static float InvertFloat(float value, float max)
    {
        return max - value;
    }
}
