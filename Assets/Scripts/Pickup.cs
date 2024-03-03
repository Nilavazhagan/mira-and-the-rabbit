using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public Item item;
    public Lore[] loreBits;

    public static OnItemPickup onPickup;

    bool playerInRange;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if(item != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = item.sprite;
        }
        InputManager.Instance.onPickup += OnPickup;
    }

    void OnPickup()
    {
        if (playerInRange)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if(item != null)
        {
            Inventory.Instance.Add(item);
            Inventory.Instance.ListAllItems();
        }
        else
        {
            foreach (Lore lore in loreBits)
            {
                LoreManager.Instance.Add(lore);
            }
        }
        onPickup?.Invoke(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}

public delegate void OnItemPickup(GameObject pickupObj);