using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnHomeButton()
    {
        SceneManager.LoadScene("Home Screen");
    }
}
