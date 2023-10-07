using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeScreenController : MonoBehaviour
{
    public void OnHomeButton()
    {
        SceneManager.LoadScene("Home Screen");
    }
}
