using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName = "ScriptableObjects/New Shop Item", order = -1)]
public class ShopItemSO : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;
    public int amountAvailable;
    public int amountOwned;
    public Sprite itemImg;
    public Sprite currencyImg;
    public Sprite getItemImg()
    {
        return itemImg;
    }
    public Sprite getCurrencyImg()
    {
        return currencyImg;
    }
    public int getAmountAvailable()
    {
        return amountAvailable;
    }
    public int getAmountOwned()
    {
        return amountOwned;
    }
}