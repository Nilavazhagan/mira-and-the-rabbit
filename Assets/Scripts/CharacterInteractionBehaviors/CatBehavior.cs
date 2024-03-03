using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehavior : Interactable
{
    public Conversation blakeIntroConversation;
    public Transform sceneEntryTarget;
    [Space(30)]
    public Conversation introConversation;
    [Space(30)]
    public Conversation milkCarton_YES;
    public Conversation milkCarton_NO;
    public Item milkCarton;
    [Space(30)]
    public BlakeBehavior blake;
    public Conversation finalConversation;
    [Space(30)]
    public Conversation foxCertConversation;
    public Transform posNearVM;
    [Space(30)]
    public GameObject teardropSigil;
    [Space(30)]
    public PanelGroupBehavior panelGroup;
    public Animator lorePanel;
    [Space(30)]
    public GameObject franticNote;
    [HideInInspector]
    public bool isNearVM = false;

    Conversation lastPlayedConversation;
    private void Start()
    {
        DialogueManager.Instance.OnConversationComplete += ConvComplete;
        Pickup.onPickup += OnItemPickup;
    }

    bool franticNotePickedUp = false;
    void OnItemPickup(GameObject pickupObj)
    {
        if(pickupObj == franticNote)
        {
            franticNotePickedUp = true;
            Interact();     //Supposed to play final conversation
        }
    }

    public override void Interact()
    {
        if (lastPlayedConversation == null)
        {
            DialogueManager.Instance.ShowConversation(introConversation, this);
            lastPlayedConversation = introConversation;
            GameManager.Instance.floor.size = Vector2.one;
            GameManager.Instance.floor.offset = Vector2.zero;
        }else if(lastPlayedConversation == introConversation)
        {
            if (Inventory.Instance.isEquipped(milkCarton))
            {
                DialogueManager.Instance.ShowConversation(milkCarton_YES, this);
                lastPlayedConversation = milkCarton_YES;
                blake.PrepareToMoveToCat();
            }
            else if(!isNearVM)
            {
                DialogueManager.Instance.ShowConversation(milkCarton_NO, this);
            }
        }else if(lastPlayedConversation == milkCarton_YES && franticNotePickedUp)
        {
            DialogueManager.Instance.ShowConversation(finalConversation, this);
            lastPlayedConversation = finalConversation;
        }
    }

    void ConvComplete(Conversation conversation)
    {
        if(conversation == blakeIntroConversation)
        {
            waitForLorePanel = true;
            panelGroup.onPanelClose += OnPanelClose;
        }
        else if(conversation == foxCertConversation)
        {
            Teleport(posNearVM);
            isNearVM = true;
        }else if(conversation == finalConversation)
        {
            teardropSigil.SetActive(teardropSigil);
        }
    }

    bool waitForLorePanel = false;
    void OnPanelClose(Animator panel)
    {
        if (waitForLorePanel && panel == lorePanel)
        {
            StartCoroutine(MoveTo(sceneEntryTarget.position));
            panelGroup.onPanelClose -= OnPanelClose;
        }
    }
}