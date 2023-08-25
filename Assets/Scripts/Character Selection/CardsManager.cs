using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AKCS
{
    public class CardsManager : MonoBehaviour
    {
        public Cards card;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI nameText, numberText;
        [SerializeField] private GameObject SelectedUI;
        Toggle toggle;
        TeamManager teamManager;
        int idx;
        
        // Start is called before the first frame update
        void Start()
        {
            SelectedUI.SetActive(false);
            toggle = GetComponent<Toggle>();

            teamManager = FindObjectOfType<TeamManager>();

            //set image and name for the UI
            image.sprite = card.image;
            nameText.text = card.charaName;

            toggle.onValueChanged.AddListener(Selected);

            if(TeamManager.selectionMode == SelectionMode.Single)
            {
                toggle.group = CharacterList._toggleGroup;
            }   
            //unset the toggle group
            else
            {
                toggle.group = null;
            }
        }
        private void LateUpdate() {
            if(TeamManager.selectionMode == SelectionMode.Multiple)
            {
                //get the index of card in temp team list
                int idx = teamManager.tempListTeam.IndexOf(card);
                if(idx!=-1)
                    numberText.text = (idx+1).ToString(); //to set the number
            }
            else
            {
                numberText.text = "";
            }
        }
        private void OnEnable() {
            //
            if(card != null)
            {
                image.sprite = card.image;
                nameText.text = card.charaName;
            }
            //set the toggle group for the toggle
            if(toggle != null)
            {
                //
                //set the toggle group
                if(TeamManager.selectionMode == SelectionMode.Single)
                {
                    toggle.group = CharacterList._toggleGroup;
                }   
                //unset the toggle group
                else
                {
                    toggle.group = null;
                }

                //check if the card is in real team list
                //SetIsOnWithoutNotify is for Set isOn without invoking onValueChanged code
                //
                int idx = teamManager.listTeam.IndexOf(card);
                if(idx != -1)
                {
                    //set selected ui to true
                    SelectedUI.SetActive(true);

                    if(TeamManager.selectionMode == SelectionMode.Multiple)
                    {
                        //set the toggle to true if the card already in real team list
                        //and also add it to temp list team
                        toggle.SetIsOnWithoutNotify(true);
                        return;
                    }
                    else
                    {
                        //just set isOn true if the idx is the same with the card we click in TeamCanvas UI
                        if(idx == SlotManager.index)
                        {
                            toggle.SetIsOnWithoutNotify(true);
                            return;
                        }
                    }
                }
                //set the toggle to off if the card is not in real team list
                toggle.SetIsOnWithoutNotify(false);
            }
        }
        private void OnDisable() 
        {
            //set selected ui to false
            SelectedUI.SetActive(false);//
            if(toggle != null)
                toggle.SetIsOnWithoutNotify(false); //
        }

        void Selected(bool on)
        {
            if(on)
            {
                if(TeamManager.selectionMode == SelectionMode.Multiple)
                {
                    //"<" because array start from 0
                    if(teamManager.tempListTeam.Count < SlotManager.teamMaxSize)
                    {
                        //to show the info about the character
                        CharaDetail.cardDetail = card;
                        SelectedUI.SetActive(true);

                        //
                        // if(teamManager.tempListTeam.Contains(card))
                        // {
                        //     return;
                        // }
                        //to show if the character is already selected for player

                        //add card to character list
                        //templistteam for save the selected card before confirmation
                        teamManager.tempListTeam.Add(card);
                        return;
                    }
                }
                //
                else
                {
                    CharaDetail.cardDetail = card;
                    SelectedUI.SetActive(true);

                    if(SlotManager.index == -1)
                    {
                        //if we click a blank card UI(at TeamCanvas), we will add it to templistteam
                        teamManager.tempListTeam.Add(card);
                    }
                    else
                    {
                        //if we click a card UI that has image, it will change the card we click to the new one
                        //in the same position
                        teamManager.tempListTeam[SlotManager.index] = card;
                    }
                    return;
                }
                toggle.isOn = false;
            }
            else
            {
                idx = teamManager.tempListTeam.IndexOf(card);
                if(TeamManager.selectionMode == SelectionMode.Multiple)
                {
                    if(idx != -1)
                    {
                        CharaDetail.cardDetail = null;
                        SelectedUI.SetActive(false);
                        teamManager.tempListTeam.Remove(card);
                    }
                }
                //
                else
                {
                    if(idx != -1)
                    {
                        CharaDetail.cardDetail = null;
                        SelectedUI.SetActive(false);

                        //if we click a card that has image than set the value for index in temp list team to null
                        if(SlotManager.index != -1)
                        {
                            teamManager.tempListTeam[SlotManager.index] = null;
                            return;
                        }
                        //just remove it if we click a blank ui card
                        teamManager.tempListTeam.Remove(card);
                    }
                }
            }
        }
    }
}
