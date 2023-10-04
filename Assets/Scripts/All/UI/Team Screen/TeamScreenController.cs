using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamScreenController : MonoBehaviour
{
    public void OnHomeButton()
    {
        SceneManager.LoadScene("Home Screen");
    }
}
