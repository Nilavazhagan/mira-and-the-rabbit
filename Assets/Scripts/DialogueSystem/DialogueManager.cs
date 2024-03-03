using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : Singleton<DialogueManager>
{
    public Text charNameText, dialogueText;
    public GameObject dialoguePanel;

    public ConversationComplete OnConversationComplete;
    Conversation currentConversation;

    Animator dialoguePanelAnimator;
    Image dialoguePanelImage;

    Queue<Dialogue> dialogues;
    Character currentNPC;

    private void Start()
    {
        dialoguePanelAnimator = dialoguePanel.GetComponent<Animator>();
        dialoguePanelImage = dialoguePanel.GetComponent<Image>();
    }

    public void ShowConversation(Conversation conversation)
    {

        dialogues = new Queue<Dialogue>(conversation.dialogues);
        currentConversation = conversation;

        InputManager.Instance.Freeze();

        DisplayNextDialogue();
        FocusContinueButton();
        UpdateAnimParam(true);

    }

    public void ShowConversation(Conversation conversation, Character npc)
    {
        currentNPC = npc;
        ShowConversation(conversation);
    }

    void FocusContinueButton()
    {
        //TODO Look for a better way or cache references
        (GameObject.FindObjectOfType(typeof(EventSystem)) as EventSystem).SetSelectedGameObject(dialoguePanelAnimator.gameObject.GetComponentInChildren<Button>().gameObject);
    }

    Dialogue nextDialogue;
    public void DisplayNextDialogue()
    {
        if(WriteDialogueCoroutine != null)
        {
            dialogueText.text = nextDialogue.sentence;
            StopCoroutine(WriteDialogueCoroutine);
            WriteDialogueCoroutine = null;
            return;
        }
        
        if (dialogues.Count <= 0)
        {
            UpdateAnimParam(false);
            InputManager.Instance.UnFreeze();
            OnConversationComplete?.Invoke(currentConversation);
            return;
        }

        nextDialogue = dialogues.Dequeue();
        if(nextDialogue.charcterType == CharType.Self)
        {
            charNameText.text = currentNPC.charName;
            dialoguePanelImage.sprite = currentNPC.dialogueBox;
        }
        else
        {
            charNameText.text = nextDialogue.charcterType == CharType.Narrator ? "" : Character.main.charName;
            dialoguePanelImage.sprite = Character.main.dialogueBox;
        }

        WriteDialogueCoroutine = StartCoroutine(WriteDialogue(nextDialogue.sentence));
    }


    Coroutine WriteDialogueCoroutine;
    IEnumerator WriteDialogue(string dialogue)
    {
        string writtenDialogue = "";
        foreach(char i in dialogue)
        {
            writtenDialogue += i;
            dialogueText.text = writtenDialogue;
            yield return new WaitForSeconds(0.05f);
        }
        WriteDialogueCoroutine = null;
    }

    void UpdateAnimParam(bool isDialogueOpen)
    {
        dialoguePanelAnimator.SetBool("isOpen", isDialogueOpen);
    }
}

public delegate void ConversationComplete(Conversation conversation);
