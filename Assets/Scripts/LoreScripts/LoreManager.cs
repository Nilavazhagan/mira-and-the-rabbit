using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoreManager : Singleton<LoreManager>
{
    public Text loreTitle, loreDescription;

    List<Lore> loreItems;

    private void Start()
    {
        loreItems = new List<Lore>();
    }

    public void Add(Lore loreItem)
    {
        if(!loreItems.Contains(loreItem))
            loreItems.Add(loreItem);

        if (loreItems.Count == 1)
            ShowLoreContent(0);
    }

    public void LoreItemClick()
    {
        int index = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
        ShowLoreContent(index);
    }

    void ShowLoreContent(int index)
    {
        if(index >= loreItems.Count)
        {
            loreTitle.text = "";
            loreDescription.text = "";
        }
        else
        {
            Lore item = loreItems[index];
            loreTitle.text = item.title;
            loreDescription.text = item.description;
        }
    }

    public void ShowLoreContent(Lore lore)
    {
        int index = loreItems.Count;
        if (loreItems.Contains(lore))
        {
            index = loreItems.IndexOf(lore);
        }

        ShowLoreContent(index);
    }
}
