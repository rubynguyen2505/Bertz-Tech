using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitManager : MonoBehaviour
{
    public Card[] cards;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Transform parent1;
    [SerializeField] private Transform parent2;
    public GameObject SelectionCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<UnitCardManager> unitCardListManager = new List<UnitCardManager>();
    public List<Card> cardList = new List<Card>();
    GameObject cardUI1, cardUI2;
    UnitCardManager unitCardManager1, unitCardManager2;

    void Awake()
    {
        cardGO.Clear();
        unitCardListManager.Clear();
        cardList.Clear();

        SelectionCanvas.SetActive(true);
        cards = Resources.LoadAll<Card>("Character Cards");
        if (parent1 == parent2)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].unlocked)
                {
                    cardUI1 = Instantiate(cardUIPrefab, parent1.position, Quaternion.identity) as GameObject;
                    cardUI1.transform.localScale = new Vector3(1, 1, 1);
                    cardUI1.transform.SetParent(parent1);
                    unitCardManager1 = cardUI1.GetComponent<UnitCardManager>();
                    unitCardManager1.card = cards[i];

                    cardGO.Add(cardUI1);
                    unitCardListManager.Add(unitCardManager1);
                    cardList.Add(cards[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].unlocked)
                {
                    cardUI1 = Instantiate(cardUIPrefab, parent1.position, Quaternion.identity) as GameObject;
                    cardUI2 = Instantiate(cardUIPrefab, parent2.position, Quaternion.identity) as GameObject;
                    cardUI1.transform.localScale = new Vector3(1, 1, 1);
                    cardUI2.transform.localScale = new Vector3(1, 1, 1);
                    cardUI1.transform.SetParent(parent1);
                    cardUI2.transform.SetParent(parent2);
                    unitCardManager1 = cardUI1.GetComponent<UnitCardManager>();
                    unitCardManager2 = cardUI2.GetComponent<UnitCardManager>();
                    unitCardManager1.card = cards[i];
                    unitCardManager2.card = cards[i];

                    cardGO.Add(cardUI1);
                    unitCardListManager.Add(unitCardManager1);
                    cardList.Add(cards[i]);
                }
            }
        }

        if (parent1 == parent2)
        {
            SelectionCanvas.SetActive(true);
        }
        else
        {
            SelectionCanvas.SetActive(false);
        }
            
    }
}
