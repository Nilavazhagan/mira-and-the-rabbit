using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2ExitBehavior : MonoBehaviour
{
    public Conversation exitConversation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("This girl is trying to run away.");
            DialogueManager.Instance.ShowConversation(exitConversation);
        }
    }
}
