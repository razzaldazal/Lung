using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class AortaClottingController : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    public Slider progressSlider;
    public TextMeshProUGUI stageText;
    public RawImage displayImage;
    public TextMeshProUGUI[] stageSpecificTexts;
    public float textDisplayDuration = 2f;

    private int currentVideoIndex = 0;
    private float[] transitionPoints;
    private float textDisplayTimer = 0f;

    void Start()
    {
        if (videoPlayers.Length != 5)
        {
            Debug.LogError("Please assign exactly 5 video players.");
            return;
        }

        if (stageSpecificTexts.Length != 5)
        {
            Debug.LogError("Please assign exactly 5 stage-specific text objects.");
            return;
        }

        transitionPoints = new float[videoPlayers.Length];
        for (int i = 0; i < videoPlayers.Length; i++)
        {
            transitionPoints[i] = (float)i / (videoPlayers.Length - 1);
        }

        progressSlider.onValueChanged.AddListener(OnSliderValueChanged);

        for (int i = 0; i < videoPlayers.Length; i++)
        {
            int index = i;
            videoPlayers[i].prepareCompleted += (source) => OnVideoPrepared(source, index);
            videoPlayers[i].errorReceived += OnVideoError;
            videoPlayers[i].Prepare();
        }

        foreach (var text in stageSpecificTexts)
        {
            text.gameObject.SetActive(false);
        }

        UpdateVideoAndText(0);
    }

    void OnVideoPrepared(VideoPlayer source, int index)
    {
        Debug.Log($"Video {index} prepared successfully");
    }

    void OnVideoError(VideoPlayer source, string message)
    {
        Debug.LogError($"Video error: {message}");
    }

    void OnSliderValueChanged(float value)
    {
        UpdateVideoAndText(value);
    }

    void UpdateVideoAndText(float sliderValue)
    {
        int newVideoIndex = Mathf.FloorToInt(sliderValue * (videoPlayers.Length - 1));

        if (newVideoIndex != currentVideoIndex)
        {
            videoPlayers[currentVideoIndex].Pause();
            currentVideoIndex = newVideoIndex;
            ShowStageText(currentVideoIndex);
        }

        float videoProgress = (sliderValue - ((float)currentVideoIndex / (videoPlayers.Length - 1))) * (videoPlayers.Length - 1);

        VideoPlayer currentPlayer = videoPlayers[currentVideoIndex];

        if (currentPlayer.isPrepared)
        {
            currentPlayer.time = videoProgress * currentPlayer.clip.length;
            currentPlayer.Play();

            if (displayImage != null && currentPlayer.texture != null)
            {
                displayImage.texture = currentPlayer.texture;
            }
        }
        else
        {
            Debug.LogWarning($"Video {currentVideoIndex} is not prepared yet");
        }

        UpdateStageText();
    }

    void UpdateStageText()
    {
        int stageNumber = currentVideoIndex + 1;
        stageText.text = "Stage: " + stageNumber;
    }

    void ShowStageText(int stageIndex)
    {
        foreach (var text in stageSpecificTexts)
        {
            text.gameObject.SetActive(false);
        }

        stageSpecificTexts[stageIndex].gameObject.SetActive(true);
        textDisplayTimer = textDisplayDuration;
    }

    void Update()
    {
        if (textDisplayTimer > 0)
        {
            textDisplayTimer -= Time.deltaTime;
            if (textDisplayTimer <= 0)
            {
                stageSpecificTexts[currentVideoIndex].gameObject.SetActive(false);
            }
        }
    }
}
