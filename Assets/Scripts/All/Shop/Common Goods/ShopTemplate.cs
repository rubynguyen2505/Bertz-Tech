using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTemplate : MonoBehaviour
{
    [SerializeField] Image itemImg;
    [SerializeField] Image currencyImg;
    public ShopItemSO shopItem;
    public TMP_Text titleTxt;
    public TMP_Text descriptionTxt;
    public int amountAvailable;
    public int amountOwned;
    public enum CurrencyType
    {
        coins,
        gems
    };
    public CurrencyType currency;
    public TMP_Text costTxt;

    void Awake()
    {
        DisplayImg();
    }

    void DisplayImg()
    {
        currencyImg.sprite = shopItem.GetCurrencyImg();
        itemImg.sprite = shopItem.GetItemImg();
    }   

    
}


