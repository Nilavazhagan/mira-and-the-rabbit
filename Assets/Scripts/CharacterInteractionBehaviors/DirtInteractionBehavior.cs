using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtInteractionBehavior : Interactable
{
    [Space(30)]
    public Conversation noShovel;
    public Item shovel;
    [Space(30)]
    public Sprite dirtGone;
    public GameObject coin;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        if (Inventory.Instance.isEquipped(shovel))
        {
            spriteRenderer.sprite = dirtGone;
            coin.SetActive(true);
        }
        else
        {
            DialogueManager.Instance.ShowConversation(noShovel);
        }
    }
}
