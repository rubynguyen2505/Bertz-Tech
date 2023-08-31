using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnStartButton(){
        SceneManager.LoadScene("Splash");
    }

    public void OnQuitButton(){
        Application.Quit();
    }

    public void OnReturnButton(){
        SceneManager.LoadScene("Title Screen");
    }
}
