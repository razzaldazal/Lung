using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button menuButton;
    public RectTransform menuButtonRect;
    public Slider slider1;
    public Slider slider2;

    private Vector2 originalPosition;
    private Vector2 topRightPosition;
    private bool isExpanded = false;

    void Start()
    {
        originalPosition = menuButtonRect.anchoredPosition;
        topRightPosition = new Vector2(Screen.width / 2 - 50, Screen.height / 2 - 50);
        slider1.gameObject.SetActive(false);
        slider2.gameObject.SetActive(false);
        menuButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            menuButtonRect.anchoredPosition = topRightPosition;
            slider1.gameObject.SetActive(true);
            slider2.gameObject.SetActive(true);
        }
        else
        {
            menuButtonRect.anchoredPosition = originalPosition;
            slider1.gameObject.SetActive(false);
            slider2.gameObject.SetActive(false);
        }
    }
}
