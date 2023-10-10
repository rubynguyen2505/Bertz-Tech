using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolveCharManager : MonoBehaviour
{
    public Card[] cards;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Transform parent;
    public GameObject SelectionCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<EvolveCardManager> evolveCardListManager = new List<EvolveCardManager>();
    public List<Card> cardList = new List<Card>();
    GameObject cardUI;
    EvolveCardManager evolveCardManager;

    void Awake()
    {
        cardGO.Clear();
        evolveCardListManager.Clear();
        cardList.Clear();

        SelectionCanvas.SetActive(true);
        cards = Resources.LoadAll<Card>("Character Cards");
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].unlocked)
            {
                //cards[i].lv = 1;
                cards[i]._hp = cards[i].hp * cards[i].lv;
                cards[i]._atk = cards[i].atk * cards[i].lv;
                cards[i]._def = cards[i].def * cards[i].lv;
                cardUI = Instantiate(cardUIPrefab, parent.position, Quaternion.identity) as GameObject;
                cardUI.transform.localScale = new Vector3(1, 1, 1);
                cardUI.transform.SetParent(parent);
                evolveCardManager = cardUI.GetComponent<EvolveCardManager>();
                evolveCardManager.card = cards[i];

                cardGO.Add(cardUI);
                evolveCardListManager.Add(evolveCardManager);
                cardList.Add(cards[i]);
            }
        }

        SelectionCanvas.SetActive(false);

    }
}
