using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryTemplate : MonoBehaviour
{
    public InventoryItemSSO inventoryItem;
    public TMP_Text title;
    public Sprite itemImg;
    void Start()
    {
        title.text = inventoryItem.itemName;
    }
    
}
