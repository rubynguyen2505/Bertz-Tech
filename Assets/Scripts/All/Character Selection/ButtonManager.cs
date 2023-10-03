using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject TeamCanvasUI;
    TeamManager teamManager;

    private void Start()
    {
        teamManager = FindObjectOfType<TeamManager>();
    }
    public void ConfirmButtom()
    {
        //clear the list team and re-add the team again with the temp list team
        teamManager.teamList.Clear();
        foreach (Card c in teamManager.tempTeamList)
        {
            //if in temp list team has null value, dont add it to list team
            //because in cardmanager for single selection its posible to have null value
            if (c != null)
                teamManager.teamList.Add(c);
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
