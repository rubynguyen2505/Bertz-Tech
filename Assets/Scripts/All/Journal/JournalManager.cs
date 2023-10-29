using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class JournalManager : MonoBehaviour
{
    public Card[] cards;
    public static Card card;

    [SerializeField] private Image charFull;

    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private GameObject infoUIPrefab;

    [SerializeField] private Transform parent1;
    [SerializeField] private Transform parent2;

    public GameObject SelectionCanvas;
    public GameObject InfoCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<JournalCardManager> journalCardListManager = new List<JournalCardManager>();
    public List<Card> cardList = new List<Card>();

    GameObject cardUI;
    GameObject infoUI;
    JournalCardManager journalCardManager;
    JournalInfoManager journalInfoManager;
    void Awake()
    {
        SelectionCanvas.SetActive(true);
        cards = Resources.LoadAll<Card>("Character Cards");
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].unlocked)
            {
                cardUI = Instantiate(cardUIPrefab, parent1.position, Quaternion.identity) as GameObject;
                cardUI.transform.localScale = new Vector3(1, 1, 1);
                cardUI.transform.SetParent(parent1);
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

        charFull.sprite = card.charFull;

        for (int i = 0; i < card.unitFacts.Length; i++)
        {
            if (card.lv >= (20 * (i + 1)))
            {
                card.isUnlockedFacts[i] = true;
            }
        }

        for (int i = 0; i < card.unitFacts.Length; i++)
        {
            infoUI = Instantiate(infoUIPrefab, parent2.position, Quaternion.identity) as GameObject;
            infoUI.transform.localScale = new Vector3(1, 1, 1);
            infoUI.transform.SetParent(parent2);
            journalInfoManager = infoUI.GetComponent<JournalInfoManager>();
            if (card.isUnlockedFacts[i] == true)
            {
                journalInfoManager.infoText.text = card.unitFacts[i].ToString();
            }
            else
            {
                journalInfoManager.infoText.text = "Locked";
            }
        }
    }
}
