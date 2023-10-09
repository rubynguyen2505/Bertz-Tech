using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabController : MonoBehaviour
{
    public GameObject upgradeCanvas, buttonCanvas;
    public GameObject SelectionCanvas;
    public GameObject upgradeTab, evolveTab;

    void Awake()
    {
        upgradeCanvas.SetActive(false);
    }
    public void OnUpgradeTab()
    {
        buttonCanvas.SetActive(false);
        SelectionCanvas.SetActive(true);
        upgradeTab.SetActive(true);
        evolveTab.SetActive(false);
    }

    public void OnEvolveTab()
    {
        buttonCanvas.SetActive(false);
        SelectionCanvas.SetActive(true);
        evolveTab.SetActive(true);
        upgradeTab.SetActive(false);
    }
}
