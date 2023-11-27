using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Item/Create New Item")]

public class InventoryItemSSO : ScriptableObject
{
    public int id;
    public int amount;
    public string itemName;
    public int price;
    public Sprite icon;
    public bool usableInInventory;
    public string description;
    [SerializeField]
    private string dbName;
    public string GetDBName()
    {
        return dbName;
    }
}
