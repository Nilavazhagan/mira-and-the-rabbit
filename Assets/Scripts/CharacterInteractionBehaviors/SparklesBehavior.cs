using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparklesBehavior : Interactable
{
    public GameObject itemsToSpawn;
    public Conversation sparklesConv;

    public override void Interact()
    {
        itemsToSpawn.SetActive(true);
        DialogueManager.Instance.ShowConversation(sparklesConv);
    }
}
