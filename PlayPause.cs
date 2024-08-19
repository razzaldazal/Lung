using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeManager : MonoBehaviour
{
    public Button playButton;
    public Button pauseButton;
    private bool isGamePaused = false;

    void Start()
    {
        playButton.onClick.AddListener(ToggleFreeze);
        pauseButton.onClick.AddListener(ToggleFreeze);
        ShowPlayButton(); 
        // Initially show the play button
    }

    void ToggleFreeze()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1f; 
            ShowPlayButton();
            // Unfreeze the game
        }
        else
        {
            Time.timeScale = 0f; 
            ShowPauseButton();
            // Freeze the game
        }
        isGamePaused = !isGamePaused;
    }

    void ShowPlayButton()
    {
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    void ShowPauseButton()
    {
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }
}
