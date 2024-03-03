using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehavior : Interactable
{
    public Conversation introConversation;
    [Space(30)]
    public Conversation apple_YES;
    public Conversation apple_NO;
    public Item apple;
    [Space(30)]
    public Conversation coin_YES;
    public Conversation coin_NO;
    public Item goldCoin;
    [Space(30)]
    public Conversation riddleWait;
    [Space(30)]
    public Conversation finalConversation;
    public Conversation finalConversationShort;
    public Item teardropSigil;
    public PanelGroupBehavior panelGroup;
    public Animator riddlePanel;
    
    [Header("Lore Items")]
    [Space(30)]
    public Lore certificate;

    [HideInInspector]
    public bool interactionComplete = false;

    Conversation lastPlayedConversation;

    private void Start()
    {
        DialogueManager.Instance.OnConversationComplete += ConvComplete;
    }

    private void ConvComplete(Conversation conversation)
    {
        if(conversation == finalConversation || conversation == finalConversationShort)
        {
            panelGroup.OpenPanel(riddlePanel);
        }
    }

    public override void Interact()
    {
        if (interactionComplete) return;

        if(lastPlayedConversation == null)
        {
            DialogueManager.Instance.ShowConversation(introConversation,this);
            lastPlayedConversation = introConversation;
        }else if(lastPlayedConversation == introConversation)
        {
            if(Inventory.Instance.isEquipped(apple))
            {
                DialogueManager.Instance.ShowConversation(apple_YES,this);
                lastPlayedConversation = apple_YES;
            }
            else
            {
                DialogueManager.Instance.ShowConversation(apple_NO,this);
            }
        }else if(lastPlayedConversation == apple_YES)
        {
            if (Inventory.Instance.isEquipped(goldCoin))
            {
                DialogueManager.Instance.ShowConversation(coin_YES,this);
                lastPlayedConversation = coin_YES;
                LoreManager.Instance.Add(certificate);
            }
            else
            {
                DialogueManager.Instance.ShowConversation(coin_NO,this);
            }
        }else if(lastPlayedConversation == coin_YES)
        {
            if (Inventory.Instance.HasItem(teardropSigil))
            {
                DialogueManager.Instance.ShowConversation(finalConversation, this);
                lastPlayedConversation = finalConversation;
            }
            else
            {
                DialogueManager.Instance.ShowConversation(riddleWait, this);
            }
        }else if(lastPlayedConversation == finalConversation)
        {
            DialogueManager.Instance.ShowConversation(finalConversationShort, this);
        }
    }
}
