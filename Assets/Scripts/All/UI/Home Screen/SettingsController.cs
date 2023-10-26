using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SettingsController : MonoBehaviour
{

    public GameObject settingsCanvas, homeCanvas;

    public void onHomeButton()
    {
        settingsCanvas.SetActive(false);
        homeCanvas.SetActive(true);
    }
}
