using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Panel currentPanel;
    private List<Panel> panelHistory = new List<Panel>();

    private void Start() 
    {
        SetupPanels();   
    }

    private void Update() 
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            GoToPrevious();
        }
    }

    private void SetupPanels()
    {
        Panel[] panels = GetComponentsInChildren<Panel>();

        foreach (Panel panel in panels)
        {
            panel.Setup(this);
        }

        currentPanel.Show();
    }

    public void GoToPrevious()
    {
        int lastIndex = panelHistory.Count - 1;

        if(panelHistory.Count == 0)
        {
            OVRManager.PlatformUIConfirmQuit();
            return;
        }

        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void SetCurrentWithHistory(Panel newPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
    }

    private void SetCurrent(Panel newPanel)
    {
        currentPanel.Hide();

        currentPanel = newPanel;
        currentPanel.Show();
    }
}