using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public Image clock;

    public bool isRunning { get; private set; }

    public void StartTimer(float time)
    {
        StartTimer(time, () => { });
    }

    public void StartTimer(float time, Action onFinish)
    {
        StopTimer();
        StartCoroutine(IETimer(time, onFinish));
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        isRunning = false;
        clock.fillAmount = 0;
    }

private IEnumerator IETimer(float time, Action onFinish)
    {
        isRunning = true;
        float currentTime = 0;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            clock.fillAmount = currentTime / time;

            yield return new WaitForEndOfFrame();
        }
        isRunning = false;
        onFinish();
    }

}
