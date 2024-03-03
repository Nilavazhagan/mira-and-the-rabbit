using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTBehavior : MonoBehaviour
{
    public PanelGroupBehavior panelGroup;
    public Animator lorePanel;
    public Lore DOT;
    public Conversation blakeRevealConv;
    public BlakeBehavior blake;

    // Start is called before the first frame update
    void Start()
    {
        Pickup.onPickup += OnItemPickup;    
    }

    void OnItemPickup(GameObject pickupObj)
    {
        if(pickupObj == gameObject)
        {
            LoreManager.Instance.ShowLoreContent(DOT);
            panelGroup.onPanelClose += OnClosePanel;
            panelGroup.OpenPanel(lorePanel);
            Pickup.onPickup -= OnItemPickup;
        }
    }

    void OnClosePanel(Animator panel)
    {
        blake.charName = "Blake";
        DialogueManager.Instance.ShowConversation(blakeRevealConv,blake);
        panelGroup.onPanelClose -= OnClosePanel;
    }
}
