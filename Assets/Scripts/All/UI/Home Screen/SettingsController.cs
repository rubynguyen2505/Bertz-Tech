using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class SettingsController : MonoBehaviour
{
    private Image[] spriteRenderers;
    public GameObject settingsCanvas, homeCanvas;
    public AudioSource backgroundMusic;

    [Header("Volume Setting")]
    [SerializeField] private TextMeshProUGUI volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Graphics Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TextMeshProUGUI brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1.0f;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private int _qualityLevel;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        /*
        settingsCanvas.SetActive(true);
        spriteRenderers = FindObjectsOfType<Image>();
        settingsCanvas.SetActive(false);
        */
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentresolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i ++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentresolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentresolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("Volume"))
        {
            if (PlayerPrefs.GetInt("Volume") == 0)
            {
                backgroundMusic.Play();
                backgroundMusic.volume = 0;
            }
            else
            {
                backgroundMusic.Play();
                backgroundMusic.volume = 1.0f;
            }
        }
        else
        {
            backgroundMusic.Play();
            backgroundMusic.volume = 1.0f;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void onHomeButton()
    {
        settingsCanvas.SetActive(false);
        homeCanvas.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        //backgroundMusic.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        //Show Prompt
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicApply()
    {
        PlayerPrefs.SetFloat("MasterBrightness", _brightnessLevel);
        /*
        foreach (Image spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = new Color(_brightnessLevel, _brightnessLevel, _brightnessLevel, spriteRenderer.color.a);
        }
        */

        // Change our brightness with your postprocessing
        Screen.brightness = _brightnessLevel;
        PlayerPrefs.SetInt("MasterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            // Reset brightness value
            brightnessSlider.value = _brightnessLevel;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicApply();
        }

        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            //backgroundMusic.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
