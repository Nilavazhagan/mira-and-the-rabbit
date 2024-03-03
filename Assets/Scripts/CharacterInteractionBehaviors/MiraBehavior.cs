using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiraBehavior : MonoBehaviour
{

    public Conversation introConversation;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.Freeze();
        Invoke("PlayIntroConversation", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayIntroConversation()
    {
        DialogueManager.Instance.ShowConversation(introConversation);
    }
}
