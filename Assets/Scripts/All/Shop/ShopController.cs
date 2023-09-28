using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    public int currency;
    public TMP_Text currencyUI;
    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] purchaseButtons;

    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        currencyUI.text = "Currency: " + currency.ToString();
        loadPanels();
        CheckPurchaseable();
    }
    void Update()
    {
        
    }
    //Temporary function to test shop
    public void addCurrency()
    {
        currency++;
        currencyUI.text = "Currency: " + currency.ToString();
        CheckPurchaseable();
    }
    //Only allow purchases that user has currency to make
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length;i++)
        {
            if (currency >= shopItemsSO[i].baseCost)
            {
                purchaseButtons[i].interactable = true;
            }
            else
            {
                purchaseButtons[i].interactable= false;
            }
        }
    }
    //Initialize shop panels
    public void loadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = shopItemsSO[i].baseCost.ToString();
        }
    }
    //Purchase an item
    public void purchaseItem(int btnNm)
    {
        if (currency >= shopItemsSO[btnNm].baseCost)
        {
            currency = currency - shopItemsSO[btnNm].baseCost;
            currencyUI.text = "Currency: " + currency.ToString();
            CheckPurchaseable();
        }
    }
}
