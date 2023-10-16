using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Item/Create New Item")]

public class InventoryItemSSO : ScriptableObject
{
    public int id;
    public string itemName;
    public int price;
    public Sprite icon;
    public bool usableInInventory;
}
