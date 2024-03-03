using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RiddlePanel : MonoBehaviour
{
    public Text PWText;
    public Color originalColor, correctColor, errorColor;
    public GameObject magnetSigil;
    public PanelGroupBehavior panelGroup;
    public FoxBehavior fox;
    public Button[] letterButtons;

    string currentPW = "";
    int runningCount = 0;

    string correctPW = "S I L E N C E";
    public void LetterPressed(string letter)
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        if (runningCount == 0)
        {
            currentPW = letter;
        }
        else if (runningCount == 7)
        {
            return;
        }
        else
        {
            currentPW += " " + letter;
        }
        runningCount++;
        PWText.text = currentPW;
        if (runningCount == 7)
        {
            if (currentPW == correctPW)
            {
                PWText.color = correctColor;
                Invoke("SpawnMagnetSigil", 1f);
            }
            else
            {
                PWText.color = errorColor;
                Invoke("ResetPW", 1f);
            }
        }
    }

    private void SpawnMagnetSigil()
    {
        panelGroup.CloseAllPanels();
        magnetSigil.SetActive(true);
        fox.interactionComplete = true;
    }

    private void ResetPW()
    {
        PWText.text = "";
        PWText.color = originalColor;
        currentPW = "";
        runningCount = 0;

        foreach (Button letterButton in letterButtons)
        {
            letterButton.interactable = true;
        }
    }
}
