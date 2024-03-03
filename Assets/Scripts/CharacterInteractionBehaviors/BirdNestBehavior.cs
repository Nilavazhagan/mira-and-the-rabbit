using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdNestBehavior : Interactable
{
    public Conversation birdNestConv;

    public override void Interact()
    {
        DialogueManager.Instance.ShowConversation(birdNestConv);
    }
}
