using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterList : MonoBehaviour
{
    Card[] cards;
    CharacterManager characterManager;
    TeamManager teamManager;

    [SerializeField] private ToggleGroup toggleGroup;
    public static ToggleGroup _toggleGroup;
    private void Awake()
    {
        teamManager = FindObjectOfType<TeamManager>();
        characterManager = FindObjectOfType<CharacterManager>();
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
        cards = characterManager.cardList.OrderBy(_card => _card.inTeam ? 0 : 1).ThenBy(_card => teamManager.teamList.IndexOf(_card)).ToArray();

        if (TeamManager.selectionMode == SelectionMode.Multiple)
        {
            int idx = 0;
            foreach (Card c in cards)
            {
                characterManager.cardGO[idx].SetActive(true);
                characterManager.cardListManager[idx].card = c;
                idx++;
            }
        }
        else
        {
            int idx = 0;
            foreach (Card c in cards)
            {
                characterManager.cardGO[idx].SetActive(true);
                characterManager.cardListManager[idx].card = c;
                if (teamManager.teamList.IndexOf(c) == SlotManager.index)
                {
                    characterManager.cardGO[idx].SetActive(true);
                }
                else if (teamManager.teamList.IndexOf(c) != -1)
                {
                    characterManager.cardGO[idx].SetActive(false);
                }
                idx++;
            }
        }
    }
}
