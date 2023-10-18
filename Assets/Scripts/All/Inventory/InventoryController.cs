using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public List<InventoryItemSSO> Items = new List<InventoryItemSSO>();
    public Transform ItemContent;
    public InventoryItemSSO[] inventoryItemsSO;
    public InventoryTemplate[] inventoryItems;
    public TMP_Text ItemDescriptionText;
    public GameObject InventoryItem, ItemDescription;
    public GameObject[] inventoryItemsGO;
    public Button[] inventoryButtons;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(InventoryItemSSO item)
    {
        Items.Add(item);
    }

    public void Remove(InventoryItemSSO item)
    {
        Items.Remove(item);
    }
    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            //clean duplicates
            Destroy(item.gameObject);
        }
        foreach (InventoryItemSSO item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("Item/ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("Item/ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
    public void ShowDescription(InventoryItemSSO item)
    {
        if (item.usableInInventory == false)
        {
            ItemDescription.SetActive(true);
            ItemDescriptionText.text = item.description;
        }
    }

    public void CloseDescription()
    {
        ItemDescription.SetActive(false);
    }

    public void LoadPanels()
    {
        for (int i = 0; i < inventoryItemsSO.Length; i++)
        {
            inventoryItems[i].title.text = inventoryItemsSO[i].itemName;
            inventoryItems[i].itemImg = inventoryItemsSO[i].icon;
        }
    }

    public void SelectItem(int btn)
    {
        ShowDescription(inventoryItemsSO[btn]);
    }
    private void Start()
    {
        for(int i = 0; i < inventoryItemsSO.Length; i++)
        {
            inventoryItemsGO[i].SetActive(true);
        }
        LoadPanels();
    }
}
