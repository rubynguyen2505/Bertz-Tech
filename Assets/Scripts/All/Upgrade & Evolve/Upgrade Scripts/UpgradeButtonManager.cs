using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonManager : MonoBehaviour
{
    public GameObject TeamCanvasUI;
    UpgradeTManager upgradeTManager;

    private void Start()
    {
        upgradeTManager = FindObjectOfType<UpgradeTManager>();
    }
    public void ConfirmButtom()
    {
        //clear the list team and re-add the team again with the temp list team
        upgradeTManager.teamList.Clear();
        foreach (Card c in upgradeTManager.tempTeamList)
        {
            //if in temp list team has null value, dont add it to list team
            //because in cardmanager for single selection its posible to have null value
            if (c != null)
                upgradeTManager.teamList.Add(c);
        }

        TeamCanvasUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CancelButton()
    {
        TeamCanvasUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
