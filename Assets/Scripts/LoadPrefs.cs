using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;

    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private SettingsController settingsController;

    [Header("Volume Setting")]
    [SerializeField] private TextMeshProUGUI volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;


    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TextMeshProUGUI brightnessTextValue = null;

    [Header("Quality Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    // Start is called before the first frame update
    void Awake()
    {
        // spriteRenderers = FindObjectsOfType<SpriteRenderer>();

        if (canUse)
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("MasterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                settingsController.ResetButton("Audio");
            }

            if (PlayerPrefs.HasKey("MasterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("MasterQuality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }

            if (PlayerPrefs.HasKey("MasterBrightness"))
            {
                float localBrightness = PlayerPrefs.GetFloat("MasterBrightness");
                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
            }
        }
    }
}
