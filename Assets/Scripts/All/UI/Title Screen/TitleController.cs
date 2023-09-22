using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TitleController : MonoBehaviour
{
    public void OnStartButton(){
        SceneManager.LoadScene("Home Screen");
    }

    public void OnQuitButton(){
        Application.Quit();
    }

    public void onTitleButton(){
        SceneManager.LoadScene("Title Screen");
    }

    public void onHomeButton()
    {
        SceneManager.LoadScene("Home Screen");
    }

}
