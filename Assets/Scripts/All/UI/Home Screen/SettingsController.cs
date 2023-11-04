using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class SettingsController : MonoBehaviour
{

    public GameObject settingsCanvas, homeCanvas;
    public AudioSource backgroundMusic;

    [Header("Volume Setting")]
    [SerializeField] private TextMeshProUGUI volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    private void Start()
    {
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

    public void ResetButton(string MenuType)
    {
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
