using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public GameObject inventoryItemsPanel;
    public GameObject inventoryItemPrefab;
    public RectTransform selectionEquipped, selectionHover;
    public Text itemNameText;
    public Text itemDescText;

    List<Item> items;
    Dictionary<Item, GameObject> itemToUIMap;
    Item equippedItem;

    private void Awake()
    {
        base.Awake();
        items = new List<Item>();
        itemToUIMap = new Dictionary<Item, GameObject>();
    }

    public void Add(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);

            GameObject inventoryItem = Instantiate(inventoryItemPrefab, inventoryItemsPanel.transform);
            inventoryItem.GetComponent<Image>().sprite = item.sprite;
            inventoryItem.GetComponent<InventoryUIItemHandler>().item = item;
            inventoryItem.transform.SetSiblingIndex(inventoryItemsPanel.transform.childCount - (2 + 1));
            itemToUIMap.Add(item, inventoryItem);

            StartCoroutine(ResetItemDescText());
        }
    }

    public void Remove(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            GameObject inventoryItem;
            if (itemToUIMap.TryGetValue(item,out inventoryItem)) {
                itemToUIMap.Remove(item);
                Destroy(inventoryItem);
            }

            if(item == equippedItem)
            {
                equippedItem = null;
                MoveEquippedSelector();
            }

            StartCoroutine(ResetItemDescText());
        }
    }

    void MoveEquippedSelector()
    {
        if(equippedItem != null)
        {
            GameObject inventoryItem;
            if (itemToUIMap.TryGetValue(equippedItem, out inventoryItem))
            {
                selectionEquipped.anchoredPosition = inventoryItem.GetComponent<RectTransform>().anchoredPosition;
                selectionEquipped.gameObject.SetActive(true);
            }
        }
        else
        {
            selectionEquipped.gameObject.SetActive(false);
        }
    }

    public void EquipItem(Item item)
    {
        if (items.Contains(item))
        {
            equippedItem = item;
            MoveEquippedSelector();
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public bool isEquipped(Item item)
    {
        return HasItem(item);// && equippedItem == item;
    }

    public void ListAllItems() {
        foreach (Item item in items)
        {
            Debug.Log(item.itemName);
        }
    }

    private IEnumerator ResetItemDescText()
    {
        yield return null;
        InventoryUIItemHandler inventoryItem = inventoryItemsPanel.GetComponentInChildren<InventoryUIItemHandler>();
        if(inventoryItem != null)
        {
            itemNameText.text = inventoryItem.item.itemName;
            itemDescText.text = inventoryItem.item.itemDescription;
            selectionHover.anchoredPosition = inventoryItem.gameObject.GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            itemNameText.text = itemDescText.text = "";
        }
    }
}
