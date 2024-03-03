using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPanel : MonoBehaviour
{
    public PanelGroupBehavior panelGroup;
    public Animator inventoryPanel, lorePanel, diaryPanel, optionsPanel;

    public void OpenInventoryPanel()
    {
        panelGroup.OpenPanel(inventoryPanel);
    }

    public void OpenLorePanel()
    {
        panelGroup.OpenPanel(lorePanel);
    }

    public void OpenDiaryPanel()
    {
        panelGroup.OpenPanel(diaryPanel);
    }

    public void OpenOptionsPanel()
    {
        panelGroup.OpenPanel(optionsPanel);
    }
}
