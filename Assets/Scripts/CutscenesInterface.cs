using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutscenesInterface : MonoBehaviour
{
    public GameObject _windowText;
    public GameObject fadeBlack;

    protected Image blackScreen;
    protected bool pause = false;

    // Start is called before the first frame update
    protected void Start()
    {
        _windowText.SetActive(false);
        blackScreen = fadeBlack.GetComponent<Image>();
    }

    public void startFadeIn(float duration, float pause, Action endAction, bool isFadeOut)
    {
        StartCoroutine(FadeIn(duration, pause, endAction, isFadeOut));
    }

    public void startFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    public void startWaiting(float duration, Action endAction)
    {
        StartCoroutine(Waiting(duration, endAction));
    }

    public IEnumerator FadeIn(float fadeDuration, float pauseDuration, Action endAction, bool isFadeOut)
    {
        pause = true;
        Color color = blackScreen.color;
        float fadeTime = 0f;
        while (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, fadeTime / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }

        color.a = 1;
        blackScreen.color = color;

        float pauseTime = 0f;
        while (pauseTime < pauseDuration)
        {
            pauseTime += Time.deltaTime;
            yield return null;
        }

        endAction?.Invoke();

        if (isFadeOut)
        {
            StartCoroutine(FadeOut(fadeDuration));
        }
    }

    public IEnumerator FadeOut(float fadeDuration)
    {
        pause = true;
        Color color = blackScreen.color;
        float fadeTime = 0f;
        while (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, fadeTime / fadeDuration);
            blackScreen.color = color;
            yield return null;
        }

        color.a = 0;
        blackScreen.color = color;

        pause = false;
    }

    public IEnumerator Waiting(float waitDuration, Action endAction)
    {
        pause = true;
        yield return new WaitForSeconds(waitDuration);

        endAction?.Invoke();
        pause = false;
    }

    public void showWindowText(bool b)
    {
        _windowText.SetActive(b);
    }

    public void startDialog(int idDialog, int interruptor)
    {
        showWindowText(true);
        _windowText.GetComponent<DialogCutscene>().startDialog(idDialog, interruptor);
    }

    public bool isTalking()
    {
        return _windowText.GetComponent<DialogCutscene>().isTalking();
    }
}
