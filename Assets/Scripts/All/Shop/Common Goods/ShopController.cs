using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions; // for ContinueWithOnMainThread
using UnityEngine.SceneManagement;



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

    private string userID;
    private DatabaseReference dbReference;



    void OnEnable()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        GetCurrency();
        
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        coinsUI.text = coins.ToString("D9");
        gemsUI.text = gems.ToString("D9");
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            amount[i].text = "x" + shopItemsSO[i].GetAmountAvailable() + " (" + shopItemsSO[i].GetAmountOwned() + " Owned)";
        }
        LoadPanels();
        CheckPurchaseable();
        
        
    }

    //Temporary function to test shop
    public void AddCurrency()
    {   // commented out to test getters and setters
        //coins++;
        //gems++;

        //testing get, add, and set
        GetCurrency();
        AddOne();
        SetCurrency();


        coinsUI.text = coins.ToString("D9");
        gemsUI.text = gems.ToString("D9");
        CheckPurchaseable();
    }

    public void GetItems()
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("items").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if(task.IsFaulted)
            {
                Debug.LogError("Get Items Faulted: " + task.Exception.ToString());
            }
            else if(task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                for(int i = 0; i < shopItemsSO.Length && snapshot != null; i++)
                {
                    shopItemsSO[i].amountOwned = int.Parse(snapshot.Child(shopItemsSO[i].GetDBName()).Value.ToString());
                }
            }

        });
    }
    //Get currency
    public void GetCurrency()
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("currency").GetValueAsync().ContinueWithOnMainThread(task =>

        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError("Get currency Faulted: " + task.Exception);

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // Do something with snapshot...
                coins = int.Parse(snapshot.Child("coins").Value.ToString());
                Debug.Log("Get Coins:  " + coins);
                gems = int.Parse(snapshot.Child("gems").Value.ToString());
                Debug.Log("Get Gems:  " + gems);
            }
        });
    }
    //currency plus 1
    public void AddOne()
    {
        coins++;
        Debug.Log("plus 1 coins:  " + coins);
        gems++;
        Debug.Log("plus 1 gems:  " + gems);
    }

    //set Currency
    public void SetCurrency()
    {
        dbReference.Child("user").Child(userID).Child("currency").Child("coins").SetValueAsync(coins);
        Debug.Log("Coins sent to database");
        dbReference.Child("user").Child(userID).Child("currency").Child("gems").SetValueAsync(gems);
        Debug.Log("Gems sent to database");

    }





    //Only allow purchases that user has currency to make
    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i].currency == ShopItemSO.CurrencyType.coins) 
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
            else if (shopItemsSO[i].currency == ShopItemSO.CurrencyType.gems)
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
        if (shopItemsSO[btnNm].currency == ShopItemSO.CurrencyType.coins)
        {

        
            if (coins >= shopItemsSO[btnNm].baseCost && shopItemsSO[btnNm].amountAvailable > 0)
            {
                coins = coins - shopItemsSO[btnNm].baseCost;
                coinsUI.text = coins.ToString("D9");
                dbReference.Child("user").Child(userID).Child("currency").Child("coins").SetValueAsync(coins);

                shopItemsSO[btnNm].amountAvailable -= 1;
                shopItemsSO[btnNm].amountOwned += 1;
                dbReference.Child("user").Child(userID).Child("items").Child(shopItemsSO[btnNm].GetDBName()).SetValueAsync(shopItemsSO[btnNm].amountOwned);
                amount[btnNm].text = "x" + shopItemsSO[btnNm].GetAmountAvailable() + " (" + shopItemsSO[btnNm].GetAmountOwned() + " Owned)";
                CheckPurchaseable();
            }
        }
        else if (shopItemsSO[btnNm].currency == ShopItemSO.CurrencyType.gems)
        {
            if (gems >= shopItemsSO[btnNm].baseCost && shopItemsSO[btnNm].amountAvailable > 0)
            {
                gems -= shopItemsSO[btnNm].baseCost;
                gemsUI.text = gems.ToString("D9");
                dbReference.Child("user").Child(userID).Child("currency").Child("gems").SetValueAsync(gems);

                shopItemsSO[btnNm].amountAvailable -= 1;
                shopItemsSO[btnNm].amountOwned += 1;
                dbReference.Child("user").Child(userID).Child("items").Child(shopItemsSO[btnNm].GetDBName()).SetValueAsync(shopItemsSO[btnNm].amountOwned);
                amount[btnNm].text = "x" + shopItemsSO[btnNm].GetAmountAvailable() + " (" + shopItemsSO[btnNm].GetAmountOwned() + " Owned)";
                CheckPurchaseable();
            }
        }
    }
}
