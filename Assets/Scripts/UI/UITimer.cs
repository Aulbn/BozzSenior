using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public Image clock;

    public void StartTimer(float time)
    {
        StopAllCoroutines();
        StartCoroutine(IETimer(time));
    }

    private IEnumerator IETimer(float time)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            clock.fillAmount = currentTime / time;

            yield return new WaitForEndOfFrame();
        }
    }

}
