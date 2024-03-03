using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticMessageBehavior : MonoBehaviour
{
    public PanelGroupBehavior panelGroup;
    public Animator lorePanel;
    public Lore mysticMessage;
    public Conversation mysticMessageConv;
    // Start is called before the first frame update
    void Start()
    {
        Pickup.onPickup += OnItemPickup;
    }

    void OnItemPickup(GameObject pickupObj)
    {
        if (pickupObj == gameObject)
        {
            LoreManager.Instance.ShowLoreContent(mysticMessage);
            panelGroup.onPanelClose += OnClosePanel;
            panelGroup.OpenPanel(lorePanel);
            Pickup.onPickup -= OnItemPickup;
        }
    }

    void OnClosePanel(Animator panel)
    {
        DialogueManager.Instance.ShowConversation(mysticMessageConv);
        panelGroup.onPanelClose -= OnClosePanel;
    }
}
