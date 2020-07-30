using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private static LoadingScreen Instance;

    public GameObject panel;
    public Slider loadingBar;

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

    public static void Show() => Instance.panel.SetActive(true);
    public static void Hide() => Instance.panel.SetActive(false);

    public static void SetProgress(float percentage)
    {
        Instance.loadingBar.value = percentage;
    }
}
