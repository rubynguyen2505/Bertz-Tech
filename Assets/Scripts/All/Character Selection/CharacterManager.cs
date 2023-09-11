using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterManager : MonoBehaviour
{
    public Card[] cards;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Transform parent;
    public GameObject SelectionCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<CardManager> cardListManager = new List<CardManager>();
    public List<Card> cardList = new List<Card>();
    GameObject cardUI;
    CardManager cardManager;

    void Awake()
    {
        cardGO.Clear();
        cardListManager.Clear();
        cardList.Clear();

        SelectionCanvas.SetActive(true);
        cards = Resources.LoadAll<Card>("Card");
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].unlocked)
            {
                cardUI = Instantiate(cardUIPrefab, parent.position, Quaternion.identity) as GameObject;
                cardUI.transform.localScale = new Vector3(1, 1, 1);
                cardUI.transform.SetParent(parent);
                cardManager = cardUI.GetComponent<CardManager>();
                cardManager.card = cards[i];

                cardGO.Add(cardUI);
                cardListManager.Add(cardManager);
                cardList.Add(cards[i]);
            }
        }
        SelectionCanvas.SetActive(false);
    }
}

