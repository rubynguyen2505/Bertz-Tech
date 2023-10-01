using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MTTemplate : MonoBehaviour
{
    [SerializeField] Image itemImg;
    [SerializeField] Image currencyImg;
    public MTItemSO MTItem;
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;
    public TMP_Text costTxt;

    void Update()
    {
        DisplayImg();
    }

    void DisplayImg()
    {
        currencyImg.sprite = MTItem.getCurrencyImg();
        itemImg.sprite = MTItem.getItemImg();
    }   

    
}


