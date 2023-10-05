using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharaDetail : MonoBehaviour
{
    public static Card cardDetail;
    [SerializeField] private GameObject noInfoText;
    [SerializeField] private GameObject charaImg;

    [Header("Character Details")]
    [SerializeField] private GameObject detailUI;
    [SerializeField] private Image charFull;
    [SerializeField] private TextMeshProUGUI charaName, atk, hp, def, role, type;

    // Update is called once per frame
    void Update()
    {
        if (cardDetail == null)
        {
            charaImg.SetActive(false);
            noInfoText.SetActive(true);
            detailUI.SetActive(false);
            return;
        }
        charaImg.SetActive(true);
        noInfoText.SetActive(false);
        detailUI.SetActive(true);
        charFull.sprite = cardDetail.charFull;
        charaName.text = "NAME : " + cardDetail.name;
        atk.text = "ATK : " + cardDetail.atk;
        hp.text = "HP : " + cardDetail.hp;
        def.text = "DEF : " + cardDetail.def;
        role.text = "ROLE : " + cardDetail.role.ToString();
        type.text = "TYPE : " + cardDetail.type.ToString();
    }
}
