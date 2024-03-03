using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VMPanelScript : MonoBehaviour
{
    public Text PWText;
    public Color originalColor, correctColor, errorColor;
    public GameObject milkCarton;
    public PanelGroupBehavior panelGroup;
    public VendingMachineInteractionBehavior vendingMachine;

    string currentPW = "";
    int runningCount = 0;

    string correctPW = "13 9 12 11";
    public void NumberPressed(string number)
    {
        if(runningCount == 0)
        {
            currentPW = number;
        }
        else if(runningCount == 4)
        {
            return;
        }
        else
        {
            currentPW += " " + number;
        }
        runningCount++;
        PWText.text = currentPW;
        if(runningCount == 4)
        {
            if(currentPW == correctPW)
            {
                PWText.color = correctColor;
                Invoke("SpawnCarton", 1f);
            }
            else
            {
                PWText.color = errorColor;
                Invoke("ResetPW", 1f);
            }
        }
    }

    private void SpawnCarton()
    {
        panelGroup.CloseAllPanels();
        milkCarton.SetActive(true);
        vendingMachine.interactionComplete = true;
    }

    private void ResetPW()
    {
        PWText.text = "";
        PWText.color = originalColor;
        currentPW = "";
        runningCount = 0;
    }
}
