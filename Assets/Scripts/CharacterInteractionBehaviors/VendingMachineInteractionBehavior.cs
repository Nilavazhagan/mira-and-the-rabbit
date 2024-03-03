using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineInteractionBehavior : Interactable
{
    public CatBehavior cat;
    public Conversation catVMConversation;
    public Conversation oddMachineConversation;
    public Animator VMPanel;
    public PanelGroupBehavior panelGroup;

    [HideInInspector]
    public bool interactionComplete = false;

    bool catVMConvShown = false;

    private void Start()
    {
        DialogueManager.Instance.OnConversationComplete += ConvComplete;
    }

    public override void Interact()
    {
        if (interactionComplete) return;

        if (catVMConvShown)
        {
            panelGroup.OpenPanel(VMPanel);
            return;
        }

        if (cat.isNearVM)
        {
            DialogueManager.Instance.ShowConversation(catVMConversation, cat);
        }
        else
        {
            DialogueManager.Instance.ShowConversation(oddMachineConversation);
        }
    }

    void ConvComplete(Conversation conversation)
    {
        if(conversation == catVMConversation)
        {
            catVMConvShown = true;
            panelGroup.OpenPanel(VMPanel);
        }
    }
}
