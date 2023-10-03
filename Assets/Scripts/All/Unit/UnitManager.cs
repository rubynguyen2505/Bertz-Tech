using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitManager : MonoBehaviour
{
    public Card[] cards;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Transform parent;
    public GameObject SelectionCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<UnitCardManager> unitCardListManager = new List<UnitCardManager>();
    public List<Card> cardList = new List<Card>();
    GameObject cardUI;
    UnitCardManager unitCardManager;

    void Awake()
    {
        cardGO.Clear();
        unitCardListManager.Clear();
        cardList.Clear();

        SelectionCanvas.SetActive(true);
        cards = Resources.LoadAll<Card>("Character Cards");
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].unlocked)
            {
                cardUI = Instantiate(cardUIPrefab, parent.position, Quaternion.identity) as GameObject;
                cardUI.transform.localScale = new Vector3(1, 1, 1);
                cardUI.transform.SetParent(parent);
                unitCardManager = cardUI.GetComponent<UnitCardManager>();
                unitCardManager.card = cards[i];

                cardGO.Add(cardUI);
                unitCardListManager.Add(unitCardManager);
                cardList.Add(cards[i]);
            }
        }
        SelectionCanvas.SetActive(true);
    }
}
