using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalScreenController : MonoBehaviour
{
    public GameObject selectionCanvas, infoCanvas;
    public Transform content;
    public void OnBackButton()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        infoCanvas.SetActive(false);
        selectionCanvas.SetActive(true);
    }
}
