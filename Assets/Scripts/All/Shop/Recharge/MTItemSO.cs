using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MTMenu", menuName = "ScriptableObjects/New MT Item", order = -1)]
public class MTItemSO : ScriptableObject
{
    public string title;
    public string description;
    public double baseCost;
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
}