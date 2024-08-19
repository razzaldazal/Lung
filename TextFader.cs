using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
    public Text uiText;
    public string[] messages;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    private int currentIndex = 0;
    private bool isFading = false;

    void Start()
    {
        uiText.text = messages[currentIndex];
        StartCoroutine(FadeTextInOut());
    }

    IEnumerator FadeTextInOut()
    {
        while (true)
        {
            yield return StartCoroutine(FadeText(0f, 1f, fadeDuration));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeText(1f, 0f, fadeDuration));
            currentIndex = (currentIndex + 1) % messages.Length;
            uiText.text = messages[currentIndex];
        }
    }

    IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        isFading = true;
        float elapsedTime = 0f;
        Color color = uiText.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = alpha;
            uiText.color = color;
            yield return null;
        }

        color.a = endAlpha;
        uiText.color = color;
        isFading = false;
    }
}
