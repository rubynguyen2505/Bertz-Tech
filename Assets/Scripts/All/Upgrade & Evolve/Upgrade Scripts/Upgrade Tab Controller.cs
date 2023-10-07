using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabController : MonoBehaviour
{
    public GameObject upgradeTab, evolveTab;
    public GameObject upgradeUnit, evolveUnit;
    public GameObject upgradeMaterial, evolveMaterial;
    public Button upgradeTabButton, evolveTabButton;

    void Awake()
    {
        OnUpgradeTab();
    }

    public void OnUpgradeTab()
    {
        upgradeUnit.SetActive(true);
        evolveUnit.SetActive(false);
        upgradeTab.SetActive(true);
        evolveTab.SetActive(false);
        upgradeMaterial.SetActive(true);
        evolveMaterial.SetActive(false);
        upgradeTabButton.interactable = false; 
        evolveTabButton.interactable = true;
    }

    public void OnEvolveTab()
    {
        evolveUnit.SetActive(true);
        upgradeUnit.SetActive(false);
        evolveTab.SetActive(true);
        upgradeTab.SetActive(false);
        evolveMaterial.SetActive(true);
        upgradeMaterial.SetActive(false);
        evolveTabButton.interactable = false;
        upgradeTabButton.interactable = true;
    }
}
