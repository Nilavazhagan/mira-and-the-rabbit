using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGroupBehavior : MonoBehaviour
{
    public Animator[] animators;
    public KeyCode[] keyCodes;
    [HideInInspector]
    public OnPanelClose onPanelClose;

    private Animator currentOpenAnimator;

    private void Update()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            KeyCode keycode = keyCodes[i];
            if (Input.GetKeyDown(keycode))
            {
                OpenPanel(animators[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllPanels();
        }
    }

    public void OpenPanel(Animator panel) {
        InputManager.Instance.Freeze();
        if (currentOpenAnimator != null)
        {
            currentOpenAnimator.SetBool("isOpen", false);
            onPanelClose?.Invoke(currentOpenAnimator);
        }

        currentOpenAnimator = panel;
        currentOpenAnimator.SetBool("isOpen", true);
    }

    public void CloseAllPanels()
    {
        if(currentOpenAnimator != false)
        {
            currentOpenAnimator.SetBool("isOpen", false);
            onPanelClose?.Invoke(currentOpenAnimator);
            currentOpenAnimator = null;
        }
        InputManager.Instance.UnFreeze();
    }

    public bool isAnyPanelOpen()
    {
        return currentOpenAnimator != null;
    }
}

public delegate void OnPanelClose(Animator panel);
