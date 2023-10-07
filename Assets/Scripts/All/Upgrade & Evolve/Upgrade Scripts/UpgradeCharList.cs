using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UpgradeCharList : MonoBehaviour
{
    Card[] cards;
    UpgradeCharManager upgradeCharManager;
    UpgradeTManager upgradeTManager;

    [SerializeField] private ToggleGroup toggleGroup;
    public static ToggleGroup _toggleGroup;
    private void Awake()
    {
        upgradeTManager = FindObjectOfType<UpgradeTManager>();
        upgradeCharManager = FindObjectOfType<UpgradeCharManager>();
        _toggleGroup = toggleGroup;
    }

    private void OnEnable()
    {
        SortingCards();
    }

    void OnDisable()
    {
        SortingCards();
    }

    void SortingCards()
    {
        //set the order of card position by card.inTeam and index of card in listTeam
        //order by inTeam?0:1 is like (expression?true condition:false condition)
        //if(_card.inTeam == true) return 0
        //else return 1
        //it will make card with "inTeam" true will be in leading position
        //"ThenBy" to order by index of card in listTeam after order by "inTeam" to make it in sequence
        cards = upgradeCharManager.cardList.OrderBy(_card => _card.inTeam ? 0 : 1).ThenBy(_card => upgradeTManager.teamList.IndexOf(_card)).ToArray();

        if (UpgradeTManager.selectionMode == SelectMode.Multiple)
        {
            int idx = 0;
            foreach (Card c in cards)
            {
                upgradeCharManager.cardGO[idx].SetActive(true);
                upgradeCharManager.upgradeCardListManager[idx].card = c;
                idx++;
            }
        }
        else
        {
            int idx = 0;
            foreach (Card c in cards)
            {
                upgradeCharManager.cardGO[idx].SetActive(true);
                upgradeCharManager.upgradeCardListManager[idx].card = c;
                if (upgradeTManager.teamList.IndexOf(c) == UpgradeSlotManager.index)
                {
                    upgradeCharManager.cardGO[idx].SetActive(true);
                }
                else if (upgradeTManager.teamList.IndexOf(c) != -1)
                {
                    upgradeCharManager.cardGO[idx].SetActive(false);
                }
                idx++;
            }
        }
    }
}
