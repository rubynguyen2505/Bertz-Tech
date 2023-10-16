using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;
    public List<InventoryItemSSO> Items = new List<InventoryItemSSO>();
    public Transform ItemContent;
    public GameObject InventoryItem;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
