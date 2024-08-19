using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class InformationReferenceDisplay : MonoBehaviour
{
    [System.Serializable]
    public class InformationItem
    {
        public string information;
        public string reference;
    }

    public TextMeshProUGUI displayText;
    public List<InformationItem> informationItems = new List<InformationItem>();
    public float informationDisplayDuration = 5f;
    public float referenceDisplayDuration = 3f;
    public float fadeDuration = 1f;

    private int currentIndex = 0;

    void Start()
    {
        if (informationItems.Count > 0)
        {
            StartCoroutine(DisplaySequence());
        }
        else
        {
            Debug.LogWarning("No information items added to the list.");
        }
    }

    IEnumerator DisplaySequence()
    {
        while (true)
        {
            yield return StartCoroutine(DisplayInformation());
            yield return StartCoroutine(DisplayReference());
            currentIndex = (currentIndex + 1) % informationItems.Count;
        }
    }

    IEnumerator DisplayInformation()
    {
        yield return FadeText(informationItems[currentIndex].information);
        yield return new WaitForSeconds(informationDisplayDuration);
        yield return FadeText("");
    }

    IEnumerator DisplayReference()
    {
        yield return FadeText("Reference: " + informationItems[currentIndex].reference);
        yield return new WaitForSeconds(referenceDisplayDuration);
        yield return FadeText("");
    }

    IEnumerator FadeText(string newText)
    {
        if (displayText.text != "")
        {
            yield return FadeTextAlpha(1f, 0f);
        }

        displayText.text = newText;

        if (newText != "")
        {
            yield return FadeTextAlpha(0f, 1f);
        }
    }

    IEnumerator FadeTextAlpha(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            displayText.alpha = alpha;
            yield return null;
        }
        displayText.alpha = endAlpha;
    }
}