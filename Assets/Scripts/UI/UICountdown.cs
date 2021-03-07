using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    protected void Start()
    {
        countdownText.CrossFadeAlpha(0, 0, true);
    }

    public void StartCountdown(int time)
    {
        StartCoroutine(IECountdown(time));
    }

    private IEnumerator IECountdown(int time)
    {
        countdownText.CrossFadeAlpha(1, .1f, true);
        for (; time > 0; time--)
        {
            countdownText.text = time.ToString();
            Tweener.Pulse(this, countdownText.rectTransform, 2, 0.2f, () => { });
            yield return new WaitForSecondsRealtime(1);
        }
        countdownText.text = "Go!";
        Tweener.Pulse(this, countdownText.rectTransform, 2, 0.2f, () => { });
        yield return new WaitForSecondsRealtime(.5f);
        countdownText.CrossFadeAlpha(0, .5f, true);
    }
}
