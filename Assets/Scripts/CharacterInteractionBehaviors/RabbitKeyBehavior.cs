using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitKeyBehavior : MonoBehaviour
{
    public BlakeBehavior blake;
    public Conversation rabbitKeyConv;

    // Start is called before the first frame update
    void Start()
    {
        Pickup.onPickup += OnItemPickup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnItemPickup(GameObject pickupObj)
    {
        if (pickupObj == gameObject)
        {
            DialogueManager.Instance.ShowConversation(rabbitKeyConv, blake);
            Pickup.onPickup -= OnItemPickup;
        }
    }
}
