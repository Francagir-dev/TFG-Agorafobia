using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public CanvasGroup canvasGroup;
    public Slider slider;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvasGroup.transform.gameObject);
    }

    public void StartGame()
    {
        StartCoroutine(StartLoad());
    }

    IEnumerator StartLoad()
    {
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        float progressValue = 0f;
       
        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetValue;
    }
}