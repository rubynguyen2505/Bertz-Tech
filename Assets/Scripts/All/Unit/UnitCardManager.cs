using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCardManager : MonoBehaviour
{
    public Card card;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject SelectedUI;
    [SerializeField] private Image frame;
    [SerializeField] private Image stars;
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
        frame.sprite = card.itemFrame;
        stars.sprite = card.stars;

        toggle.onValueChanged.AddListener(Selected);

        if (TeamManager.selectionMode == SelectionMode.Single)
        {
            toggle.group = UnitList._toggleGroup;
        }
        //unset the toggle group
        else
        {
            toggle.group = null;
        }
    }

    /*
    private void LateUpdate()
    {
        if (TeamManager.selectionMode == SelectionMode.Multiple)
        {
            //get the index of card in temp team list
            int idx = teamManager.tempTeamList.IndexOf(card);
            if (idx != -1)
                numberText.text = (idx + 1).ToString(); //to set the number
        }
        else
        {
            numberText.text = "";
        }
    }
    */

    private void OnEnable()
    {
        //
        if (card != null)
        {
            image.sprite = card.image;
            nameText.text = card.charaName;
        }
        //set the toggle group for the toggle
        if (toggle != null)
        {
            //
            //set the toggle group
            if (TeamManager.selectionMode == SelectionMode.Single)
            {
                toggle.group = UnitList._toggleGroup;
            }
            //unset the toggle group
            else
            {
                toggle.group = null;
            }

            //check if the card is in real team list
            //SetIsOnWithoutNotify is for Set isOn without invoking onValueChanged code
            //
            int idx = teamManager.teamList.IndexOf(card);
            if (idx != -1)
            {
                //set selected ui to true
                SelectedUI.SetActive(true);

                if (TeamManager.selectionMode == SelectionMode.Multiple)
                {
                    //set the toggle to true if the card already in real team list
                    //and also add it to temp list team
                    toggle.SetIsOnWithoutNotify(true);
                    return;
                }
                else
                {
                    //just set isOn true if the idx is the same with the card we click in TeamCanvas UI
                    if (idx == UnitSlotManager.index)
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
        if (toggle != null)
            toggle.SetIsOnWithoutNotify(false); //
    }

    void Selected(bool on)
    {
        if (on)
        {
            CharaDetail.cardDetail = card;
            SelectedUI.SetActive(true);

            return;
        }
        else
        {

            CharaDetail.cardDetail = null;
            SelectedUI.SetActive(false);
        
        }
    }
}
