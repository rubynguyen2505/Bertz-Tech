using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





public class TitleController : MonoBehaviour
{
    public Button start;

    IEnumerator Start()
    {
        if (start != null)
        {
            start.interactable = false;
            yield return new WaitForSeconds(0.25f);
            start.interactable = true;
        }
    }
/*
    void Start()
    {
        if (start != null)
        {
            start.interactable = true;
        }
    }*/
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
