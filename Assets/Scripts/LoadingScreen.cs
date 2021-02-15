using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen Instance;

    public Image background;
    public Slider loadingBar;

    public float transitionTime;
    public AnimationCurve transitionCurve;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Hide();
    }

    public static void Show() => Instance.background.gameObject.SetActive(true);
    public static void Hide() => Instance.background.gameObject.SetActive(false);

    public static IEnumerator IEToggle(bool show)
    {
        if (show)
            Show();

        float timer = 0;
        Instance.background.fillMethod = (Image.FillMethod)Random.Range(0, 4);
        Instance.background.fillOrigin = GetRandomOriginIndex();
        

        while (timer < Instance.transitionTime)
        {
            timer += Time.deltaTime;
            Instance.background.fillAmount = Instance.transitionCurve.Evaluate( show ? (timer / Instance.transitionTime) : Tweener.InvertFloat(timer / Instance.transitionTime, 1));
            yield return new WaitForEndOfFrame();
        }
        if (!show)
            Hide();
    }

    private static int GetRandomOriginIndex()
    {
        if (Instance.background.fillMethod == Image.FillMethod.Horizontal || Instance.background.fillMethod == Image.FillMethod.Vertical)
            return Random.Range(0, 2);
        else
            return Random.Range(0, 4);
    }
        public static void SetProgress(float percentage)
    {
        Instance.loadingBar.value = percentage;
    }
}
