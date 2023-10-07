using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EvolveSlotManager : MonoBehaviour
{
    [SerializeField] private Button[] slotButton;
    EvolveSlot[] _slot;
    EvolveTManager evolveTManager;
    EvolveCharManager evolveCharManager;
    public static int teamMaxSize; //public const int teamMaxSizes = 8;
    public static int index;

    [Header("Canvas")]
    [SerializeField] private GameObject TeamCanvas;
    [SerializeField] private GameObject SelectionCanvas;
    [SerializeField] private GameObject UpgradeTab, Evolvetab;

    private void Awake()
    {
        evolveTManager = FindObjectOfType<EvolveTManager>();
        evolveCharManager = FindObjectOfType<EvolveCharManager>();
        _slot = new EvolveSlot[slotButton.Length];

        for (int i = 0; i < slotButton.Length; i++)
        {
            _slot[i] = slotButton[i].GetComponent<EvolveSlot>();
        }
        teamMaxSize = slotButton.Length;
    }
    private void OnEnable()
    {
        //set all card "inTeam" to false
        //
        for (int i = 0; i < evolveCharManager.cards.Length; i++)
        {
            evolveCharManager.cards[i].inTeam = false;
        }

        if (evolveTManager.teamList.Count > 0)
        {
            for (int i = 0; i < slotButton.Length; i++)
            {
                _slot[i].card = null;
            }

            int idx = 0;
            foreach (Card c in evolveTManager.teamList)
            {
                //set cardIdx
                _slot[idx].cardIdx = evolveTManager.teamList.IndexOf(c); //
                _slot[idx].card = c;
                idx++;

                //find the cards from characterManager with the same name with cards in listTeam
                //set the cards "inTeam" to true
                //
                Card card = Array.Find(evolveCharManager.cards, _card => _card == c);
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
        EvolveTManager.selectionMode = SelectingMode.Single; //
        index = _slot[idx].cardIdx;

        //clear the temp list team and re-add again with the list team,
        evolveTManager.tempTeamList.Clear();
        foreach (Card c in evolveTManager.teamList)
        {
            evolveTManager.tempTeamList.Add(c);
        }

        SelectionCanvas.SetActive(true);
        UpgradeTab.SetActive(false);
        Evolvetab.SetActive(true);
        TeamCanvas.SetActive(false);
    }

    public void MultiSelectButton()
    {
        //set selection mode
        EvolveTManager.selectionMode = SelectingMode.Multiple; //

        //clear the temp list team and re-add again with the list team,
        evolveTManager.tempTeamList.Clear();
        foreach (Card c in evolveTManager.teamList)
        {
            evolveTManager.tempTeamList.Add(c);
        }

        SelectionCanvas.SetActive(true);
        UpgradeTab.SetActive(false);
        Evolvetab.SetActive(true);
        TeamCanvas.SetActive(false);
    }

    public void ClearButton()
    {
        evolveTManager.teamList.Clear();
        foreach (EvolveSlot s in _slot)
        {
            s.cardIdx = -1;
            s.card = null;
            StartCoroutine(s.Show());
        }
    }
}
