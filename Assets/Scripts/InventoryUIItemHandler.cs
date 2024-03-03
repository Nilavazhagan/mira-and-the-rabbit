using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIItemHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public RectTransform selection;
    public Item item;

    private RectTransform selfRect;
    private Text itemName, itemDescription;

    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.Instance.EquipItem(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selection.anchoredPosition = selfRect.anchoredPosition;
        itemName.text = item.itemName;
        itemDescription.text = item.itemDescription;
    }

    // Start is called before the first frame update
    void Start()
    {
        selfRect = GetComponent<RectTransform>();
        itemName = Inventory.Instance.itemNameText;
        itemDescription = Inventory.Instance.itemDescText;
        selection = Inventory.Instance.selectionHover;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
