using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions; // for ContinueWithOnMainThread

public class InventoryController : MonoBehaviour
{
    public int gems;
    public int coins;
    public static InventoryController Instance;
    public List<InventoryItemSSO> Items = new List<InventoryItemSSO>();
    public Transform ItemContent;
    public InventoryItemSSO[] inventoryItemsSO;
    public InventoryTemplate[] inventoryItems;
    public TMP_Text ItemDescriptionText, ItemDescriptionTitle, ItemDescriptionAmount;
    public TMP_Text ItemUseText, ItemUseTitle, ItemUseAmount;
    public TMP_Text coinsUI;
    public TMP_Text gemsUI;
    public GameObject InventoryItem, ItemDescription, ItemUse;
    public GameObject[] inventoryItemsGO;
    public Image ItemDescriptionImg, ItemUseImg;
    public Button[] inventoryButtons;
    private string userID;
    private DatabaseReference dbReference;

    void OnEnable()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        getCurrency();
        coinsUI.text = coins.ToString("D9");
        gemsUI.text = gems.ToString("D9");
    }
    public void getCurrency()
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

    public void GetItems()
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("items").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Get Items Faulted: " + task.Exception.ToString());
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                for (int i = 0; i < inventoryItemsSO.Length && snapshot != null; i++)
                {
                    if (snapshot != null)
                    {
                        inventoryItemsSO[i].amount = int.Parse(snapshot.Child(inventoryItemsSO[i].GetDBName()).Value.ToString());
                    }
                    else
                    {
                        dbReference.Child("user").Child(userID).Child("items").Child(inventoryItemsSO[i].GetDBName()).SetRawJsonValueAsync("0");
                        inventoryItemsSO[i].amount = 0;
                    }
                }
            }

        });
    }
    private void Awake()
    {
        Instance = this;
    }

    public void SelectItem(InventoryItemSSO item)
    {
        if (item.usableInInventory == false)
        {
            ShowDescription(item);
        }
        else
        {
            ShowUseItem(item);
        }
    }
    public void ShowDescription(InventoryItemSSO item)
    {
        ItemDescription.SetActive(true);
        ItemDescriptionText.text = item.description;
        ItemDescriptionTitle.text = item.itemName;
        ItemDescriptionAmount.text = "Amount Owned: "+ item.amount.ToString();
        ItemDescriptionImg.sprite = item.icon;
    }

    public void CloseDescription()
    {
        ItemDescription.SetActive(false);
    }
    
    public void ShowUseItem(InventoryItemSSO item)
    {
        ItemUse.SetActive(true);
        ItemUseText.text = item.description;
        ItemUseTitle.text = item.itemName;
        ItemUseAmount.text = "Amount Owned: " + item.amount.ToString();
        ItemUseImg.sprite = item.icon;
    }

    public void CloseUseItem()
    {
        ItemUse.SetActive(false);
    }
    public void UseItem(InventoryItemSSO item)
    {
        Debug.Log("Energy added");
        ItemUse.SetActive(false);
        inventoryItemsSO[1].amount -= 1;
        dbReference.Child("user").Child(userID).Child("items").Child(inventoryItemsSO[1].GetDBName()).SetValueAsync(inventoryItemsSO[1].amount);
        for (int i = 0; i < inventoryItemsSO.Length; i++)
        {
            if (inventoryItemsSO[i].amount != 0)
            {
                inventoryItemsGO[i].SetActive(true);
            }
        }

    }
    public void LoadPanels()
    {
        for (int i = 0; i < inventoryItemsSO.Length; i++)
        {

            inventoryItems[i].title.text = inventoryItemsSO[i].itemName;
            inventoryItems[i].itemImg.sprite = inventoryItemsSO[i].icon;
        }
    }

    public void SelectItem(int btn)
    {
        ShowDescription(inventoryItemsSO[btn]);
    }
    private void Start()
    {
        GetItems();
        for(int i = 0; i < inventoryItemsSO.Length; i++)
        {
            if (inventoryItemsSO[i].amount != 0)
            {
                inventoryItemsGO[i].SetActive(true);
            }
        }
        LoadPanels();
    }
}
