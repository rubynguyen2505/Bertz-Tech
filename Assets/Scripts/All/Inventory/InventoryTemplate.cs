using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTemplate : MonoBehaviour
{
    public InventoryItemSSO inventoryItem;
    public TMP_Text title;
    public Image itemImg;
    void Start()
    {
        title.text = inventoryItem.itemName;
        itemImg.sprite = inventoryItem.icon;
    }
    
}
