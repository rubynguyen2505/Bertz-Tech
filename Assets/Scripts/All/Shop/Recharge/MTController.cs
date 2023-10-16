using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MTController : MonoBehaviour
{
    public double coins;
    public double gems;
    public TMP_Text coinsUI;
    public TMP_Text gemsUI;
    public MTItemSO[] MTItemsSO;
    public MTTemplate[] MTPanels;
    public GameObject[] MTPanelsGO;
    public Button[] purchaseButtons;

    void Start()
    {
        for (int i = 0; i < MTItemsSO.Length; i++)
        {
            MTPanelsGO[i].SetActive(true);
        }
        coinsUI.text = coins.ToString("D9");
        gemsUI.text = gems.ToString("D9");
        loadPanels();
/*        CheckPurchaseable();*/
    }
    void Update()
    {
        
    }
    //Temporary function to test MT
/*    public void addCurrency()
    {
        currency += .50;
        currencyUI.text = "Currency: " + currency.ToString();
        CheckPurchaseable();
    }*/
    //Only allow purchases that user has currency to make
    /*public void CheckPurchaseable()
    {
        for (int i = 0; i < MTItemsSO.Length;i++)
        {
            if (currency >= MTItemsSO[i].baseCost)
            {
                purchaseButtons[i].interactable = true;
            }
            else
            {
                purchaseButtons[i].interactable= false;
            }
        }
    }*/
    //Initialize MT panels
    public void loadPanels()
    {
        for (int i = 0; i < MTItemsSO.Length; i++)
        {
            MTPanels[i].titleTxt.text = MTItemsSO[i].title;
            MTPanels[i].descriptionTxt.text = MTItemsSO[i].description;
            MTPanels[i].costTxt.text = MTItemsSO[i].baseCost.ToString();
        }
    }
/*    //Purchase an item
    public void purchaseItem(int btnNm)
    {
        if (currency >= MTItemsSO[btnNm].baseCost)
        {
            currency = currency - MTItemsSO[btnNm].baseCost;
            currencyUI.text = "Currency: " + currency.ToString();
            CheckPurchaseable();
        }
    }*/
}
