using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnStartButton(){
        SceneManager.LoadScene("Level Select");
    }

    public void OnQuitButton(){
        Application.Quit();
    }

    public void Home(){
        SceneManager.LoadScene("Title Screen");
    }
}
