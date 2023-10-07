using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCharManager : MonoBehaviour
{
    public Card[] cards;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Transform parent;
    public GameObject SelectionCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<UpgradeCardManager> upgradeCardListManager = new List<UpgradeCardManager>();
    public List<Card> cardList = new List<Card>();
    GameObject cardUI;
    UpgradeCardManager upgradeCardManager;

    void Awake()
    {
        cardGO.Clear();
        upgradeCardListManager.Clear();
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
                upgradeCardManager = cardUI.GetComponent<UpgradeCardManager>();
                upgradeCardManager.card = cards[i];

                cardGO.Add(cardUI);
                upgradeCardListManager.Add(upgradeCardManager);
                cardList.Add(cards[i]);
            }
        }

        SelectionCanvas.SetActive(false);

    }
}
