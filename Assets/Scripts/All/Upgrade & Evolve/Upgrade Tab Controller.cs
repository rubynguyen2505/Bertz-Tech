using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabController : MonoBehaviour
{
    public GameObject upgradeTab, evolveTab;
    public Button upgradeTabButton, evolveTabButton;

    void Awake()
    {
        OnUpgradeTab();
    }

    public void OnUpgradeTab()
    {
        upgradeTab.SetActive(true);
        evolveTab.SetActive(false);
        upgradeTabButton.interactable = false; 
        evolveTabButton.interactable = true;
    }

    public void OnEvolveTab()
    {

        evolveTab.SetActive(true);
        upgradeTab.SetActive(false);
        evolveTabButton.interactable = false;
        upgradeTabButton.interactable = true;
    }
}
