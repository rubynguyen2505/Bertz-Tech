using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "ScriptableObjects/New Shop Item", order = -1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    [SerializeField] 
    private string dbName;
    public int baseCost;
    public int amountAvailable;
    public int amountOwned;
    public Sprite itemImg;
    public Sprite currencyImg;
    public enum CurrencyType
    {
        coins,
        gems
    };
    public CurrencyType currency;
    public Sprite GetItemImg()
    {
        return itemImg;
    }
    public Sprite GetCurrencyImg()
    {
        return currencyImg;
    }
    public int GetAmountAvailable()
    {
        return amountAvailable;
    }
    public int GetAmountOwned()
    {
        return amountOwned;
    }
    public CurrencyType GetCurrencyType()
    {
        return currency;
    }
    public string GetDBName()
    {
        return dbName;
    }
}