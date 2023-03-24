using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingPanel : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] Image fadeImage;
    bool fading = false;

    public void FadeIn(Action callback = null)
    {
        if (!fading) StartCoroutine(FadeInCoroutine(callback));
    }
    public void FadeOut(Action callback = null)
    {
        if (!fading) StartCoroutine(FadeOutCoroutine(callback));
    }


    IEnumerator FadeInCoroutine(Action callback)
    {
        fading = true;
        for (float c = 0; c <= 1; c += Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, c);
            yield return null;
        }
        callback?.Invoke();
        fading = false;
    }

    IEnumerator FadeOutCoroutine(Action callback)
    {
        fading = true;
        for (float c = 1; c >= 0; c -= Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, c);
            yield return new WaitForFixedUpdate();
        }
        callback?.Invoke();
        fading = false;
    }
}
