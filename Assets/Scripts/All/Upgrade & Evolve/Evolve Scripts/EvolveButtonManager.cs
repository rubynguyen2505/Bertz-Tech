using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveButtonManager : MonoBehaviour
{
    public GameObject TeamCanvasUI;
    EvolveTManager evolveTManager;

    private void Start()
    {
        evolveTManager = FindObjectOfType<EvolveTManager>();
    }
    public void ConfirmButtom()
    {
        //clear the list team and re-add the team again with the temp list team
        evolveTManager.teamList.Clear();
        foreach (Card c in evolveTManager.tempTeamList)
        {
            //if in temp list team has null value, dont add it to list team
            //because in cardmanager for single selection its posible to have null value
            if (c != null)
                evolveTManager.teamList.Add(c);
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
