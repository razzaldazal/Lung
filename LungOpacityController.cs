using UnityEngine;
using UnityEngine.UI;

public class LungOpacityController : MonoBehaviour
{
    public Slider opacitySlider;
    public Text valueText;
    public GameObject lung1;
    public GameObject lung2;

    private Renderer lung1Renderer;
    private Renderer lung2Renderer;
    private Material lung1Material;
    private Material lung2Material;

    void Start()
    {
        lung1Renderer = lung1.GetComponent<Renderer>();
        lung2Renderer = lung2.GetComponent<Renderer>();
        lung1Material = lung1Renderer.material;
        lung2Material = lung2Renderer.material;

        opacitySlider.minValue = 0;
        opacitySlider.maxValue = 100;
        opacitySlider.wholeNumbers = true;
        opacitySlider.onValueChanged.AddListener(UpdateOpacity);

        UpdateOpacity(0);
    }

    void UpdateOpacity(float value)
    {
        valueText.text = value.ToString("0");

        float opacity = 1 - (value / 100f);

        if (value < 50)
        {
            lung1.SetActive(true);
            SetLungOpacity(lung1Material, opacity * 2);
        }
        else
        {
            lung1.SetActive(false);
        }

        if (value < 100)
        {
            lung2.SetActive(true);
            SetLungOpacity(lung2Material, opacity);
        }
        else
        {
            lung2.SetActive(false);
        }
    }

    void SetLungOpacity(Material material, float opacity)
    {
        Color color = material.color;
        color.a = opacity;
        material.color = color;
    }
}
