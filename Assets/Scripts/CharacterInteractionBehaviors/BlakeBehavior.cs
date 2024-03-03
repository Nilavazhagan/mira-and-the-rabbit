using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlakeBehavior : Interactable
{
    public Conversation introConversation;
    public Transform sceneExitTransform;
    public float sceneExitDelay;
    [Space(30)]
    public float finalMovementSpeed;
    public Transform posBeforeMoveToCat;
    public Transform posNearCat;
    public Conversation catMilkConversation;
    public CatBehavior cat;
    [Space(30)]
    public PanelGroupBehavior panelGroup;
    public Animator lorePanel;
    [Space(30)]
    public GameObject storyBook;
    public GameObject franticNote;

    [Header("Lore Items")]
    [Space(30)]
    public Lore scribbledNote;
    //public Lore franticNote;
    public Lore begrudgingNote;
    public Lore declarationOfTrust;
    [Space(30)]
    public Conversation finalConversation;
    public Item owlFeather;

    Conversation lastPlayedConversation;
    float initialMovementSpeed;
    private void Start()
    {
        DialogueManager.Instance.OnConversationComplete += ConvComplete;
        Pickup.onPickup += OnItemPickup;
        initialMovementSpeed = movementSpeed;
    }
    public override void Interact()
    {
        if(lastPlayedConversation == null)
        {
            DialogueManager.Instance.ShowConversation(introConversation, this);
            lastPlayedConversation = introConversation;
        }
        else
        {
            if (Inventory.Instance.HasItem(owlFeather))
            {
                DialogueManager.Instance.ShowConversation(finalConversation);
            }
        }
    }

    void ConvComplete(Conversation conversation)
    {
        if(conversation == introConversation)
        {
            waitForLorePanel = true;
            LoreManager.Instance.Add(scribbledNote);
            panelGroup.onPanelClose += OnPanelClose;
            panelGroup.OpenPanel(lorePanel);
        }else if(conversation == catMilkConversation)
        {
            InputManager.Instance.Freeze();
            movementSpeed = finalMovementSpeed;
            StartCoroutine(MoveTo(posNearCat.position, () =>
            {
                //LoreManager.Instance.Add(franticNote);
                franticNote.SetActive(true);
                StartCoroutine(MoveTo(posBeforeMoveToCat.position, () =>
                {
                    InputManager.Instance.UnFreeze();
                    Teleport(sceneExitTransform);
                    //Invoke("StartCatConv", 0.5f);
                }));
            }));
        }else if(conversation == blakeRevealConv)
        {
            rabbitKey.SetActive(true);
        }else if(conversation == finalConversation)
        {
            GameManager.Instance.showOutro();
        }
    }

    bool waitForLorePanel = false;
    void OnPanelClose(Animator panel)
    {
        if(waitForLorePanel && panel == lorePanel)
        {
            Invoke("ExitScene", sceneExitDelay);
            panelGroup.onPanelClose -= OnPanelClose;
        }
    }

/*    void StartCatConv()
    {
        cat.Interact();
    }*/

    void ExitScene()
    {
        StartCoroutine(MoveTo(sceneExitTransform.position));
        storyBook.SetActive(true);
    }

    public void PrepareToMoveToCat()
    {
        Teleport(posBeforeMoveToCat);
    }


    [Header("Area2")]
    [Space(30)]
    public List<GameObject> area2LoreItems;
    public Transform posBeforeArea2, posInArea2;
    public GameObject declOfTrust;
    [Space(30)]
    public Conversation blakeRevealConv;
    public GameObject rabbitKey;

    void OnItemPickup(GameObject pickupObj)
    {
        if (area2LoreItems.Contains(pickupObj))
        {
            area2LoreItems.Remove(pickupObj);
            if(area2LoreItems.Count == 0)
            {
                Teleport(posBeforeArea2);
                movementSpeed = initialMovementSpeed;
                StartCoroutine(MoveTo(posInArea2.position, () =>
                {
                    declOfTrust.SetActive(true);
                }));
            }
        }
    }
}
