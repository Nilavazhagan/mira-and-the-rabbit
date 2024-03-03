using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeTreesBehavior : Interactable
{
    [Space(30)]
    public Conversation sigilNO;
    public Conversation sigilYES;

    [Space(30)]
    public Item magnetSigil;
    public Item teardropSigil;
    public Item seedSigil;

    [Space(30)]
    public GameObject activatedSigils;
    public GameObject sparkles;

    public override void Interact()
    {
        if(Inventory.Instance.HasItem(seedSigil) && Inventory.Instance.HasItem(teardropSigil) && Inventory.Instance.HasItem(seedSigil))
        {
            activatedSigils.SetActive(true);
            sparkles.SetActive(true);
            DialogueManager.Instance.ShowConversation(sigilYES);
        }
        else
        {
            DialogueManager.Instance.ShowConversation(sigilNO);
        }
    }
}
