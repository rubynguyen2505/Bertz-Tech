using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolveCardManager : MonoBehaviour
{
    public Card card;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject SelectedUI;
    [SerializeField] private Image frame;
    [SerializeField] private Image stars;
    Toggle toggle;
    EvolveTManager evolveTManager;
    int idx;

    // Start is called before the first frame update
    void Start()
    {
        SelectedUI.SetActive(false);
        toggle = GetComponent<Toggle>();

        evolveTManager = FindObjectOfType<EvolveTManager>();

        //set image and name for the UI
        image.sprite = card.image;
        nameText.text = card.charaName;
        frame.sprite = card.itemFrame;
        stars.sprite = card.stars;

        toggle.onValueChanged.AddListener(Selected);


        toggle.group = EvolveCharList._toggleGroup;

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

            toggle.group = EvolveCharList._toggleGroup;

            //check if the card is in real team list
            //SetIsOnWithoutNotify is for Set isOn without invoking onValueChanged code
            //
            int idx = evolveTManager.teamList.IndexOf(card);
            if (idx != -1)
            {
                //set selected ui to true
                SelectedUI.SetActive(true);


                toggle.SetIsOnWithoutNotify(true);
                return;

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
            EvolveCharaDetail.cardDetail = card;
            SelectedUI.SetActive(true);
            evolveTManager.tempTeamList.Add(card);
            return;
        }
        else
        {

            EvolveCharaDetail.cardDetail = null;
            SelectedUI.SetActive(false);
            evolveTManager.tempTeamList.Remove(card);
        }
    }
}
