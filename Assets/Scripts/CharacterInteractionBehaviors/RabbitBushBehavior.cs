using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitBushBehavior : Interactable
{
    [Space(30)]
    public Item rabbitKey;
    public Conversation rabbitBushConversation;
    public GameObject seedSigil;

    private void Start()
    {
        DialogueManager.Instance.OnConversationComplete += ConvComplete;
    }

    public override void Interact()
    {
        if (Inventory.Instance.isEquipped(rabbitKey))
        {
            DialogueManager.Instance.ShowConversation(rabbitBushConversation);
        }
    }

    void ConvComplete(Conversation conversation)
    {
        if(conversation == rabbitBushConversation)
        {
            seedSigil.SetActive(true);
        }
    }
}
