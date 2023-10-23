using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JournalManager : MonoBehaviour
{
    public Card[] cards;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Transform parent;
    public GameObject SelectionCanvas;
    public GameObject InfoCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<JournalCardManager> journalCardListManager = new List<JournalCardManager>();
    public List<Card> cardList = new List<Card>();
    GameObject cardUI;
    JournalCardManager journalCardManager;
    void Awake()
    {
        SelectionCanvas.SetActive(true);
        cards = Resources.LoadAll<Card>("Character Cards");
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].unlocked)
            {
                cardUI = Instantiate(cardUIPrefab, parent.position, Quaternion.identity) as GameObject;
                cardUI.transform.localScale = new Vector3(1, 1, 1);
                cardUI.transform.SetParent(parent);
                journalCardManager = cardUI.GetComponent<JournalCardManager>();
                journalCardManager.card = cards[i];

                cardGO.Add(cardUI);
                journalCardListManager.Add(journalCardManager);
                cardList.Add(cards[i]);
            }
        }
        SelectionCanvas.SetActive(true);
    }

    public void SetUI()
    {
        InfoCanvas.SetActive(true);
    }
}
