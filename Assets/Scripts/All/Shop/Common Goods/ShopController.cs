using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    public int coins;
    public int gems;
    public TMP_Text coinsUI;
    public TMP_Text gemsUI;
    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopPanels;
    public GameObject[] shopPanelsGO;
    public Button[] purchaseButtons;
    public TMP_Text[] amount;

    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        coinsUI.text = coins.ToString("D9");
        gemsUI.text = gems.ToString("D9");
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            amount[i].text = "x" + shopItemsSO[i].getAmountAvailable() + " (" + shopItemsSO[i].getAmountOwned() + " Owned)";
        }
        LoadPanels();
        CheckPurchaseable();
    }
    void Update()
    {
        
    }
    //Temporary function to test shop
    public void addCurrency()
    {
        coins++;
        gems++;
        coinsUI.text = coins.ToString("D9");
        gemsUI.text = gems.ToString("D9");
        CheckPurchaseable();
    }
    //Only allow purchases that user has currency to make
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i].currencyType == 0) 
            { 
                if (coins >= shopItemsSO[i].baseCost && shopItemsSO[i].amountAvailable > 0)
                {
                    purchaseButtons[i].interactable = true;
                }
                else
                {
                    purchaseButtons[i].interactable = false;
                }
            }
            else if (shopItemsSO[i].currencyType == 1)
            {
                if (gems >= shopItemsSO[i].baseCost && shopItemsSO[i].amountAvailable > 0)
                {
                    purchaseButtons[i].interactable = true;
                }
                else
                {
                    purchaseButtons[i].interactable = false;
                }
            }
        }
    }
    //Initialize shop panels
    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = shopItemsSO[i].baseCost.ToString();
        }
    }
    //Purchase an item
    public void PurchaseItem(int btnNm)
    {
        if (shopItemsSO[btnNm].currencyType == 0)
        {

        
            if (coins >= shopItemsSO[btnNm].baseCost && shopItemsSO[btnNm].amountAvailable > 0)
            {
                coins = coins - shopItemsSO[btnNm].baseCost;
                coinsUI.text = coins.ToString("D9");
                shopItemsSO[btnNm].amountAvailable -= 1;
                shopItemsSO[btnNm].amountOwned += 1;
                amount[btnNm].text = "x" + shopItemsSO[btnNm].getAmountAvailable() + " (" + shopItemsSO[btnNm].getAmountOwned() + " Owned)";
                CheckPurchaseable();
            }
        }
        else if (shopItemsSO[btnNm].currencyType == 1)
        {
            if (gems >= shopItemsSO[btnNm].baseCost && shopItemsSO[btnNm].amountAvailable > 0)
            {
                gems = gems - shopItemsSO[btnNm].baseCost;
                gemsUI.text = gems.ToString("D9");
                shopItemsSO[btnNm].amountAvailable -= 1;
                shopItemsSO[btnNm].amountOwned += 1;
                amount[btnNm].text = "x" + shopItemsSO[btnNm].getAmountAvailable() + " (" + shopItemsSO[btnNm].getAmountOwned() + " Owned)";
                CheckPurchaseable();
            }
        }
    }
}
