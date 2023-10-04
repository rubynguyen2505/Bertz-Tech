using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UnitSlotManager : MonoBehaviour
{
    [SerializeField] private Button[] slotButton;
    UnitSlot[] _slot;
    TeamManager teamManager;
    UnitManager unitManager;
    public static int teamMaxSize; //public const int teamMaxSizes = 8;
    public static int index;

    [Header("Canvas")]
    [SerializeField] private GameObject TeamCanvas;
    [SerializeField] private GameObject SelectionCanvas;

    private void Awake()
    {
        teamManager = FindObjectOfType<TeamManager>();
        unitManager = FindObjectOfType<UnitManager>();
        _slot = new UnitSlot[slotButton.Length];

        for (int i = 0; i < slotButton.Length; i++)
        {
            _slot[i] = slotButton[i].GetComponent<UnitSlot>();
        }
        teamMaxSize = slotButton.Length;
    }
    private void OnEnable()
    {
        //set all card "inTeam" to false
        //
        for (int i = 0; i < unitManager.cards.Length; i++)
        {
            unitManager.cards[i].inTeam = false;
        }

        if (teamManager.teamList.Count > 0)
        {
            for (int i = 0; i < slotButton.Length; i++)
            {
                _slot[i].card = null;
            }

            int idx = 0;
            foreach (Card c in teamManager.teamList)
            {
                //set cardIdx
                _slot[idx].cardIdx = teamManager.teamList.IndexOf(c); //
                _slot[idx].card = c;
                idx++;

                //find the cards from characterManager with the same name with cards in listTeam
                //set the cards "inTeam" to true
                //
                Card card = Array.Find(unitManager.cards, _card => _card == c);
                card.inTeam = true;
            }
        }
    }

    private void Start()
    {
        //set the parameter for each button with differents value for each button
        for (int i = 0; i < slotButton.Length; i++)
        {
            //int idx to give differents value for each button
            int idx = i;

            //if SelectChara(i) where i is from "for", all buttons will have same parameter
            //
            slotButton[i].onClick.AddListener(() => SelectChara(idx));
        }
    }

    void SelectChara(int idx)
    {
        //set selection mode
        TeamManager.selectionMode = SelectionMode.Single; //
        index = _slot[idx].cardIdx;

        //clear the temp list team and re-add again with the list team,
        teamManager.tempTeamList.Clear();
        foreach (Card c in teamManager.teamList)
        {
            teamManager.tempTeamList.Add(c);
        }

        SelectionCanvas.SetActive(true);
        //TeamCanvas.SetActive(false);
    }

    public void MultiSelectButton()
    {
        //set selection mode
        TeamManager.selectionMode = SelectionMode.Multiple; //

        //clear the temp list team and re-add again with the list team,
        teamManager.tempTeamList.Clear();
        foreach (Card c in teamManager.teamList)
        {
            teamManager.tempTeamList.Add(c);
        }

        SelectionCanvas.SetActive(true);
        TeamCanvas.SetActive(false);
    }

    public void ClearButton()
    {
        teamManager.teamList.Clear();
        foreach (UnitSlot s in _slot)
        {
            s.cardIdx = -1;
            s.card = null;
            StartCoroutine(s.Show());
        }
    }
}
