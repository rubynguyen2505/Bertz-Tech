using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EvolveCharList : MonoBehaviour
{
    Card[] cards;
    EvolveCharManager evolveCharManager;
    EvolveTManager evolveTManager;

    [SerializeField] private ToggleGroup toggleGroup;
    public static ToggleGroup _toggleGroup;
    private void Awake()
    {
        evolveTManager = FindObjectOfType<EvolveTManager>();
        evolveCharManager = FindObjectOfType<EvolveCharManager>();
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
        cards = evolveCharManager.cardList.OrderBy(_card => _card.inTeam ? 0 : 1).ThenBy(_card => evolveTManager.teamList.IndexOf(_card)).ToArray();

        if (EvolveTManager.selectionMode == SelectingMode.Multiple)
        {
            int idx = 0;
            foreach (Card c in cards)
            {
                evolveCharManager.cardGO[idx].SetActive(true);
                evolveCharManager.evolveCardListManager[idx].card = c;
                idx++;
            }
        }
        else
        {
            int idx = 0;
            foreach (Card c in cards)
            {
                evolveCharManager.cardGO[idx].SetActive(true);
                evolveCharManager.evolveCardListManager[idx].card = c;
                if (evolveTManager.teamList.IndexOf(c) == EvolveSlotManager.index)
                {
                    evolveCharManager.cardGO[idx].SetActive(true);
                }
                else if (evolveTManager.teamList.IndexOf(c) != -1)
                {
                    evolveCharManager.cardGO[idx].SetActive(false);
                }
                idx++;
            }
        }
    }
}
